# 📘 CSVocalize

> A simple, open-source Windows desktop app for translating Unity localization CSV files using the DeepL API.

---

## ✨ Features

- 🧠 Automatically translates the `English(en)` column to any selected languages
- 📁 Drag-and-drop or browse to select your Unity-style CSV
- 💾 Remembers your DeepL API key and preferred languages
- 🧪 Built with C# WinForms — lightweight and easy to use
- 🗂 Saves output as a `_translated.csv` alongside your original file

---

## 🚀 Getting Started

### 1. Download the App
> [Releases](https://github.com/sakne/CSVocalize/releases)

Or build it yourself from source:

### 2. Build from Source
```bash
git clone https://github.com/YOUR_USERNAME/CSVocalize.git
```
Open the solution in Visual Studio 2022+ and run AutoCSVTranslator.sln

### 🛠 How to Use
```
ℹ️ The app maps fr → French(fr), tr → Turkish(tr), etc. Columns must already exist in your file.
ℹ️ File must have english version filled to translation know what to translate.

-> Open CSVocalize

-> Click Set API Key to enter your DeepL key (stored locally).

-> Click Browse and select your Unity-compatible CSV.

-> Enter your desired language codes (e.g., fr, tr, de) into the textbox.

-> Click Translate CSV and let the app fill in your missing translations.

 -> The translated version will be saved as _translated.csv.
```

### 🤝 Contributing

Pull requests are welcome! If you find bugs or want to suggest improvements, please [open an issue](https://github.com/sakne/CSVocalize/issues).  
