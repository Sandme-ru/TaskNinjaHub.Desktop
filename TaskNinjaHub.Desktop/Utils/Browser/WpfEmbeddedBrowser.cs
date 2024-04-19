using CefSharp.Wpf;
using System.Windows;
using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace TaskNinjaHub.Desktop.Utils.Browser
{
    public class WpfEmbeddedBrowser : IBrowser
    {
        private BrowserOptions _options = null!;

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            _options = options;

            var window = new Window
            {
                Width = 900,
                Height = 625,
                Title = "Авторизация"
            };

            var chromiumWebBrowser = new ChromiumWebBrowser();

            var signal = new SemaphoreSlim(0, 1);

            var result = new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };

            chromiumWebBrowser.FrameLoadEnd += (s, e) =>
            {
                if (BrowserIsNavigatingToRedirectUri(e.Url))
                {
                    result = new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Url
                    };

                    signal.Release();

                    window.Dispatcher.Invoke(() => window.Close());
                }
            };

            window.Closing += (s, e) =>
            {
                signal.Release();
            };

            window.Content = chromiumWebBrowser;
            window.Show();
            var response = await chromiumWebBrowser.LoadUrlAsync(_options.StartUrl);

            await signal.WaitAsync(cancellationToken);

            if (!response.Success)
                MessageBox.Show(response.ErrorCode.ToString());

            return result;
        }

        private bool BrowserIsNavigatingToRedirectUri(string url)
        {
            return url.StartsWith(_options.EndUrl);
        }
    }
}