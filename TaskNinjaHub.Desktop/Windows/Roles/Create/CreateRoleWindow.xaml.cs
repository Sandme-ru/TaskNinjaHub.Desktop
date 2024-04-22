using System.Windows;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Roles.List;

namespace TaskNinjaHub.Desktop.Windows.Roles.Create
{
    /// <summary>
    /// Логика взаимодействия для CreateRoleWindow.xaml
    /// </summary>
    public partial class CreateRoleWindow : Window
    {
        private RoleService _roleService;
        public CreateRoleWindow()
        {
            InitializeComponent();
            NameTextBlock.Text = PropertyStorage.Username;
        }
        public void InjectTaskTypeService(RoleService roleService)
        {
            _roleService = roleService;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length > 0)
            {
                var result = await _roleService.AddRoleAsync(NameBox.Text);

                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Роль успешно добавлена");
                    RoleService roleService = new RoleService(new HttpClientFactory());
                    RoleListWindow window = new RoleListWindow();
                    window.InjectTaskTypeService(roleService);
                    window.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Введите название для роли");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            RoleService roleService = new RoleService(new HttpClientFactory());
            RoleListWindow window = new RoleListWindow();
            window.InjectTaskTypeService(roleService);
            window.Show();
            this.Hide();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            RoleService roleService = new RoleService(new HttpClientFactory());
            RoleListWindow window = new RoleListWindow();
            window.InjectTaskTypeService(roleService);
            window.Show();
            this.Hide();
        }
    }
}
