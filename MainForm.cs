// Required Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCSVTranslator
{
    public partial class Main : Form
    {
        private string csvPath = "";
        private string settingsPath = "settings.json";
        private AppSettings settings = new AppSettings();
        private string apiKey = "";
        private readonly HttpClient client = new HttpClient();
        private const string DEEPL_URL = "https://api-free.deepl.com/v2/translate";
        private DataTable cachedTable;
        private string cachedPath;

        public Main()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                settings = JsonSerializer.Deserialize<AppSettings>(json);
                apiKey = settings.apiKey;
                txtLog.AppendText("Loaded saved settings." + Environment.NewLine);
                if (settings.languages != null && settings.languages.Count > 0)
                {
                    txtLanguages.Text = string.Join(", ", settings.languages);
                }
            }
        }

        private void SaveSettings()
        {
            settings.apiKey = apiKey;
            settings.languages = new List<string>(txtLanguages.Text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }


        private void btnSetApiKey_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter your DeepL API Key:", "API Key", apiKey);
            if (!string.IsNullOrWhiteSpace(input))
            {
                apiKey = input;
                txtLog.AppendText("API key saved." + Environment.NewLine);
                SaveSettings();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvPath = openFileDialog.FileName;
                txtLog.AppendText($"Selected file: {Path.GetFileName(csvPath)}" + Environment.NewLine);
            }
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                MessageBox.Show("Please set your DeepL API key first.");
                return;
            }
            if (string.IsNullOrWhiteSpace(csvPath) || !File.Exists(csvPath))
            {
                MessageBox.Show("Please select a CSV file.");
                return;
            }

            var langs = txtLanguages.Text.Split(',');
            var langList = new List<string>();
            foreach (var lang in langs)
            {
                var clean = lang.Trim();
                if (!string.IsNullOrWhiteSpace(clean))
                    langList.Add(clean);
            }
            if (langList.Count == 0)
            {
                MessageBox.Show("Please enter at least one language code.");
                return;
            }

            if (cachedTable != null && cachedPath == csvPath)
            {
                txtLog.AppendText("Using cached CSV data." + Environment.NewLine);
            }
            else
            {
                cachedTable = new DataTable();
                using (var reader = new StreamReader(csvPath))
                {
                    string headerLine = await reader.ReadLineAsync();
                    if (headerLine == null)
                    {
                        MessageBox.Show("CSV is empty.");
                        return;
                    }

                    string[] headers = headerLine.Split(',');
                    foreach (var header in headers)
                        cachedTable.Columns.Add(header);

                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line.Split(',');
                        cachedTable.Rows.Add(values);
                    }
                }
                cachedPath = csvPath;
            }

            var table = cachedTable;
            if (!table.Columns.Contains("English(en)"))
            {
                MessageBox.Show("Missing 'English(en)' column.");
                return;
            }

            // Map language codes to column names like fr -> French(fr)
            Dictionary<string, string> columnMap = new Dictionary<string, string>();
            foreach (DataColumn col in table.Columns)
            {
                foreach (var lang in langList)
                {
                    if (col.ColumnName.EndsWith($"({lang})"))
                    {
                        columnMap[lang] = col.ColumnName;
                    }
                }
            }

            foreach (var lang in langList)
            {
                if (!columnMap.ContainsKey(lang))
                {
                    MessageBox.Show($"No column found for language: {lang} (expected something like 'French({lang})')");
                    continue;
                }

                string colName = columnMap[lang];

                foreach (DataRow row in table.Rows)
                {
                    var english = row["English(en)"]?.ToString();
                    if (string.IsNullOrWhiteSpace(english)) continue;
                    if (!string.IsNullOrWhiteSpace(row[colName]?.ToString())) continue;

                    string translated = await TranslateTextAsync(english, lang);
                    row[colName] = translated;
                    txtLog.AppendText($"{english} -> {translated} [{colName}]" + Environment.NewLine);
                    await Task.Delay(1200);
                }
            }

            string outputPath = Path.Combine(Path.GetDirectoryName(csvPath),
                Path.GetFileNameWithoutExtension(csvPath) + "_translated.csv");
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sb.Append(table.Columns[i].ColumnName);
                if (i < table.Columns.Count - 1) sb.Append(",");
            }
            sb.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sb.Append(row[i]);
                    if (i < table.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();
            }

            File.WriteAllText(outputPath, sb.ToString());
            MessageBox.Show($"Translation complete! Saved to: {outputPath}");
        }

        private async Task<string> TranslateTextAsync(string text, string targetLang)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("target_lang", targetLang.ToUpper())
            });

            var response = await client.PostAsync(DEEPL_URL, content);
            var jsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(jsonString);
                return doc.RootElement.GetProperty("translations")[0].GetProperty("text").GetString();
            }
            else
            {
                txtLog.AppendText($"Error translating: {jsonString}" + Environment.NewLine);
                return "";
            }
        }

        private void CS_LL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://store.steampowered.com/app/3453530/Coffie_Simulator/");
        }

        private void SakneDev_LL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/sakne");
        }

        private void OpenUrl(string url)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open URL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class AppSettings
    {
        public string apiKey { get; set; }
        public List<string> languages { get; set; } = new List<string>();
    }

}
