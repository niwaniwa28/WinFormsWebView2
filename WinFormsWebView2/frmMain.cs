using Microsoft.Web.WebView2.Core;
using System.ComponentModel;

namespace WinFormsWebView2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            // WebView2�����������C�x���g�ǉ�
            this.webViewMain.CoreWebView2InitializationCompleted += WebViewMain_InitializationCompleted;
            this.webViewMain.EnsureCoreWebView2Async(null);
        }

        /// <summary>
        /// WebView2�����������C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebViewMain_InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                //// �t�H���_���h���C���Ɋ��蓖�Ă�
                //this.webViewMain.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.example", "localhtml", CoreWebView2HostResourceAccessKind.Allow);

                //// URL�ɑJ��
                //this.webViewMain.CoreWebView2.Navigate("https://appassets.example/index.html");

                // URL�ɑJ��
                this.webViewMain.CoreWebView2.Navigate("http://localhost:4200");

                // �J�ڊ����̃C�x���g�ǉ�
                this.webViewMain.CoreWebView2.NavigationCompleted += WebViewMain_NavigationCompleted;

                // �J�ڂ̃C�x���g�ǉ�
                this.webViewMain.CoreWebView2.SourceChanged += WebViewMain_SourceChanged;
            }
            else
            {
                // �G���[����
            }
        }

        /// <summary>
        /// �ǂݍ��݊����C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WebViewMain_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                this.txtUrl.Text = this.webViewMain.CoreWebView2.Source;

                //// JavaScript���s
                //this.webViewMain.ExecuteScriptAsync("document.getElementsByName('hoge').item(0).value = 'C#';");

                // ���
                Thread.Sleep(500);
                CoreWebView2PrintSettings settings = this.webViewMain.CoreWebView2.Environment.CreatePrintSettings();
                settings.PrinterName = "Microsoft Print to PDF";
                settings.ShouldPrintHeaderAndFooter = false;
                CoreWebView2PrintStatus printStatus = await this.webViewMain.CoreWebView2.PrintAsync(settings);
            }
            else
            {
                // �G���[����
            }
        }

        /// <summary>
        /// URL�ω��C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebViewMain_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
        {
            this.txtUrl.Text = this.webViewMain.CoreWebView2.Source;
        }
    }
}