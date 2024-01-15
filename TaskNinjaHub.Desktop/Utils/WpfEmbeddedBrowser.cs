using CefSharp;
using CefSharp.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace TaskNinjaHub.Desktop.Utils
{
    public class WpfEmbeddedBrowser : IBrowser
    {
        private BrowserOptions _options = null;

        public WpfEmbeddedBrowser()
        {

        }

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
            chromiumWebBrowser.Load(_options.StartUrl);

            await signal.WaitAsync(cancellationToken);

            return result;
        }

        private bool BrowserIsNavigatingToRedirectUri(string url)
        {
            return url.StartsWith(_options.EndUrl);
        }
    }
}