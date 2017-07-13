namespace ParseXML
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textXML = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblURL = new System.Windows.Forms.Label();
            this.textURL = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(18, 16);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1142, 677);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textXML);
            this.tabPage1.Controls.Add(this.labelStatus);
            this.tabPage1.Controls.Add(this.textStatus);
            this.tabPage1.Controls.Add(this.btnGenerate);
            this.tabPage1.Controls.Add(this.lblURL);
            this.tabPage1.Controls.Add(this.textURL);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1134, 645);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parse XML";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textXML
            // 
            this.textXML.Location = new System.Drawing.Point(14, 42);
            this.textXML.Multiline = true;
            this.textXML.Name = "textXML";
            this.textXML.Size = new System.Drawing.Size(100, 20);
            this.textXML.TabIndex = 12;
            this.textXML.Text = resources.GetString("textXML.Text");
            this.textXML.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(9, 42);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(1108, 29);
            this.labelStatus.TabIndex = 11;
            this.labelStatus.Text = "No Status";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStatus.UseMnemonic = false;
            // 
            // textStatus
            // 
            this.textStatus.Location = new System.Drawing.Point(14, 76);
            this.textStatus.Margin = new System.Windows.Forms.Padding(4);
            this.textStatus.Multiline = true;
            this.textStatus.Name = "textStatus";
            this.textStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textStatus.Size = new System.Drawing.Size(1105, 557);
            this.textStatus.TabIndex = 6;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(963, 5);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(154, 33);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Parse";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(9, 14);
            this.lblURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(108, 19);
            this.lblURL.TabIndex = 3;
            this.lblURL.Text = "Input URL :";
            // 
            // textURL
            // 
            this.textURL.Location = new System.Drawing.Point(125, 9);
            this.textURL.Margin = new System.Windows.Forms.Padding(4);
            this.textURL.Name = "textURL";
            this.textURL.Size = new System.Drawing.Size(827, 26);
            this.textURL.TabIndex = 4;
            this.textURL.Text = resources.GetString("textURL.Text");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listFiles);
            this.tabPage2.Controls.Add(this.webBrowser);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1134, 645);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Exported Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listFiles
            // 
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 19;
            this.listFiles.Location = new System.Drawing.Point(7, 9);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(394, 631);
            this.listFiles.TabIndex = 13;
            // 
            // webBrowser
            // 
            this.webBrowser.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.webBrowser.CausesValidation = false;
            this.webBrowser.Location = new System.Drawing.Point(408, 9);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.webBrowser.MinimumSize = new System.Drawing.Size(30, 29);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(717, 628);
            this.webBrowser.TabIndex = 11;
            this.webBrowser.TabStop = false;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 705);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "Parse XML from external URL";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox textURL;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox textXML;
        private System.Windows.Forms.ListBox listFiles;
    }
}

