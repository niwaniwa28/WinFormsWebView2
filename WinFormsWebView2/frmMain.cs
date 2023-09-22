using Microsoft.Web.WebView2.Core;
using System.ComponentModel;

namespace WinFormsWebView2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            // WebView2初期化完了イベント追加
            this.webViewMain.CoreWebView2InitializationCompleted += WebViewMain_InitializationCompleted;
            this.webViewMain.EnsureCoreWebView2Async(null);
        }

        /// <summary>
        /// WebView2初期化完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebViewMain_InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                //// フォルダをドメインに割り当てる
                //this.webViewMain.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.example", "localhtml", CoreWebView2HostResourceAccessKind.Allow);

                //// URLに遷移
                //this.webViewMain.CoreWebView2.Navigate("https://appassets.example/index.html");

                // URLに遷移
                this.webViewMain.CoreWebView2.Navigate("http://localhost:4200");

                // 遷移完了のイベント追加
                this.webViewMain.CoreWebView2.NavigationCompleted += WebViewMain_NavigationCompleted;

                // 遷移のイベント追加
                this.webViewMain.CoreWebView2.SourceChanged += WebViewMain_SourceChanged;
            }
            else
            {
                // エラー処理
            }
        }

        /// <summary>
        /// 読み込み完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void WebViewMain_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                this.txtUrl.Text = this.webViewMain.CoreWebView2.Source;

                //// JavaScript実行
                //this.webViewMain.ExecuteScriptAsync("document.getElementsByName('hoge').item(0).value = 'C#';");

                // 印刷
                Thread.Sleep(500);
                CoreWebView2PrintSettings settings = this.webViewMain.CoreWebView2.Environment.CreatePrintSettings();
                settings.PrinterName = "Microsoft Print to PDF";
                settings.ShouldPrintHeaderAndFooter = false;
                CoreWebView2PrintStatus printStatus = await this.webViewMain.CoreWebView2.PrintAsync(settings);
            }
            else
            {
                // エラー処理
            }
        }

        /// <summary>
        /// URL変化イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebViewMain_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
        {
            this.txtUrl.Text = this.webViewMain.CoreWebView2.Source;
        }
    }
}