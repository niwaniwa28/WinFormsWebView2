namespace WinFormsWebView2
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webViewMain = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUrl = new TextBox();
            ((System.ComponentModel.ISupportInitialize)webViewMain).BeginInit();
            SuspendLayout();
            // 
            // webViewMain
            // 
            webViewMain.AllowExternalDrop = true;
            webViewMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webViewMain.CreationProperties = null;
            webViewMain.DefaultBackgroundColor = Color.White;
            webViewMain.Location = new Point(0, 41);
            webViewMain.Name = "webViewMain";
            webViewMain.Size = new Size(800, 409);
            webViewMain.TabIndex = 0;
            webViewMain.ZoomFactor = 1D;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Location = new Point(12, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(776, 23);
            txtUrl.TabIndex = 1;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtUrl);
            Controls.Add(webViewMain);
            Name = "frmMain";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)webViewMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewMain;
        private TextBox txtUrl;
    }
}