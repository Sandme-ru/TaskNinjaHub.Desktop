using IdentityModel.OidcClient;
using System.Windows;
using TaskNinjaHub.Desktop.Services.UserProviderService;
using TaskNinjaHub.Desktop.Utils.Browser;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Menu;

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
                Authority = "https://auth.sandme.ru",
                ClientId = "TaskNinjaHub",
                ClientSecret = "",
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
                UserProvider.GetUser(result.AccessToken);
                var name = result.User.Identity?.Name;

                PropertyStorage.Username = UserProvider.User.Name;

                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                this.Hide();
                mainMenuWindow.Show();
            }
        }
    }
}