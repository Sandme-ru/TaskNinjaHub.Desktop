using IdentityModel.OidcClient;
using System.Windows;
using TaskNinjaHub.Desktop.Services;
using TaskNinjaHub.Desktop.Utils;

namespace TaskNinjaHub.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OidcClient? _oidcClient = null!;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Start;
        }

        public async void Start(object sender, RoutedEventArgs e)
        {
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:7219/",
                ClientId = "TaskNinjaHub",
                ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3655",
                Scope = "openid profile email",
                RedirectUri = "http://127.0.0.1/TaskNinjaHub.Desktop",
                Browser = new WpfEmbeddedBrowser()
            };

            _oidcClient = new OidcClient(options);

            LoginResult result;

            try
            {
                result = await _oidcClient.LoginAsync();
            }
            catch (Exception ex)
            {
                Message.Text = $"Unexpected Error: {ex.Message}";
                return;
            }

            if (result.IsError)
            {
                Message.Text = result.Error == "UserCancel" ? "The sign-in window was closed before authorization was completed." : result.Error;
            }
            else
            {
                var name = result.User.Identity?.Name;
                UserProvider.User = result.User.Claims;
                Message.Text = $"Hello {name}";
            }
        }
    }
}