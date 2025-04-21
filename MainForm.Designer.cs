namespace AutoCSVTranslator
{
    partial class Main
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnSetApiKey;
        private System.Windows.Forms.TextBox txtLanguages;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLang;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            btnBrowse = new Button();
            btnTranslate = new Button();
            btnSetApiKey = new Button();
            txtLanguages = new TextBox();
            txtLog = new TextBox();
            lblLang = new Label();
            SakneDev_LL = new LinkLabel();
            CS_LL = new LinkLabel();
            SuspendLayout();
            // 
            // btnBrowse
            // 
            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            btnBrowse.Font = new Font("Roboto Lt", 9F, FontStyle.Bold);
            btnBrowse.ForeColor = Color.White;
            btnBrowse.Location = new Point(20, 20);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(150, 30);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "Select CSV File";
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnTranslate
            // 
            btnTranslate.BackColor = Color.FromArgb(33, 52, 72);
            btnTranslate.Font = new Font("Roboto Lt", 9F, FontStyle.Bold);
            btnTranslate.ForeColor = Color.White;
            btnTranslate.Location = new Point(162, 124);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(150, 30);
            btnTranslate.TabIndex = 4;
            btnTranslate.Text = "Translate CSV";
            btnTranslate.UseVisualStyleBackColor = false;
            btnTranslate.Click += btnTranslate_Click;
            // 
            // btnSetApiKey
            // 
            btnSetApiKey.Font = new Font("Roboto Lt", 9F, FontStyle.Bold);
            btnSetApiKey.ForeColor = Color.White;
            btnSetApiKey.Location = new Point(330, 20);
            btnSetApiKey.Name = "btnSetApiKey";
            btnSetApiKey.Size = new Size(150, 30);
            btnSetApiKey.TabIndex = 1;
            btnSetApiKey.Text = "Set API Key";
            btnSetApiKey.Click += btnSetApiKey_Click;
            // 
            // txtLanguages
            // 
            txtLanguages.BackColor = Color.FromArgb(148, 180, 193);
            txtLanguages.BorderStyle = BorderStyle.FixedSingle;
            txtLanguages.ForeColor = Color.Silver;
            txtLanguages.Location = new Point(20, 95);
            txtLanguages.Name = "txtLanguages";
            txtLanguages.Size = new Size(460, 22);
            txtLanguages.TabIndex = 3;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(33, 52, 72);
            txtLog.BorderStyle = BorderStyle.None;
            txtLog.Font = new Font("Roboto Lt", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            txtLog.ForeColor = Color.White;
            txtLog.Location = new Point(20, 170);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(460, 200);
            txtLog.TabIndex = 5;
            txtLog.TextAlign = HorizontalAlignment.Center;
            // 
            // lblLang
            // 
            lblLang.BackColor = Color.FromArgb(0, 3, 65, 200);
            lblLang.FlatStyle = FlatStyle.Popup;
            lblLang.Font = new Font("Roboto Lt", 9F, FontStyle.Bold);
            lblLang.ForeColor = Color.White;
            lblLang.Location = new Point(20, 70);
            lblLang.Name = "lblLang";
            lblLang.Size = new Size(400, 20);
            lblLang.TabIndex = 2;
            lblLang.Text = "Enter languages (e.g., tr, fr, de):";
            // 
            // SakneDev_LL
            // 
            SakneDev_LL.ActiveLinkColor = Color.RosyBrown;
            SakneDev_LL.AutoSize = true;
            SakneDev_LL.LinkBehavior = LinkBehavior.NeverUnderline;
            SakneDev_LL.LinkColor = Color.Bisque;
            SakneDev_LL.Location = new Point(412, 377);
            SakneDev_LL.Name = "SakneDev_LL";
            SakneDev_LL.Size = new Size(68, 14);
            SakneDev_LL.TabIndex = 6;
            SakneDev_LL.TabStop = true;
            SakneDev_LL.Text = "SakneDev";
            SakneDev_LL.TextAlign = ContentAlignment.MiddleCenter;
            SakneDev_LL.LinkClicked += SakneDev_LL_LinkClicked;
            // 
            // CS_LL
            // 
            CS_LL.ActiveLinkColor = Color.RosyBrown;
            CS_LL.AutoSize = true;
            CS_LL.LinkBehavior = LinkBehavior.NeverUnderline;
            CS_LL.LinkColor = Color.Bisque;
            CS_LL.Location = new Point(292, 377);
            CS_LL.Name = "CS_LL";
            CS_LL.Size = new Size(110, 14);
            CS_LL.TabIndex = 7;
            CS_LL.TabStop = true;
            CS_LL.Text = "Coffie Simulator";
            CS_LL.TextAlign = ContentAlignment.MiddleCenter;
            CS_LL.LinkClicked += CS_LL_LinkClicked;
            // 
            // Main
            // 
            BackColor = Color.FromArgb(33, 52, 72);
            ClientSize = new Size(500, 400);
            Controls.Add(CS_LL);
            Controls.Add(SakneDev_LL);
            Controls.Add(btnBrowse);
            Controls.Add(btnSetApiKey);
            Controls.Add(lblLang);
            Controls.Add(txtLanguages);
            Controls.Add(btnTranslate);
            Controls.Add(txtLog);
            Font = new Font("Roboto Lt", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            Text = "CSVocalize";
            ResumeLayout(false);
            PerformLayout();
        }
        private LinkLabel SakneDev_LL;
        private LinkLabel CS_LL;
    }
}
