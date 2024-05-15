using System.Net;
using System.Windows;
using TaskNinjaHub.Desktop.Models.Priorities;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Priorities.List;
using TaskNinjaHub.Desktop.Windows.Roles.List;

namespace TaskNinjaHub.Desktop.Windows.Roles.Update
{
    /// <summary>
    /// Логика взаимодействия для UpdateRoleWindow.xaml
    /// </summary>
    public partial class UpdateRoleWindow : Window
    {
        private RoleService _roleService;

        private Role _role;

        public UpdateRoleWindow()
        {
            InitializeComponent();
            _role = RoleListWindow.Role;
            NameBox.Text = _role.Name;
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
                var exisiRoles = await _roleService.GetAllAsync();
                if (exisiRoles.Any(x => x.Name == NameBox.Text))
                {
                    MessageBox.Show("Такая роль уже существует");
                    return;
                }

                _role.Name = NameBox.Text;

                var result = await _roleService.EditRoleAsync(_role.Id.ToString(), _role.Name);

                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Роль успешно обновлена");
                    RoleService roleService = new RoleService(new HttpClientFactory());
                    RoleListWindow window = new RoleListWindow();
                    window.InjectTaskTypeService(roleService);
                    window.Show();
                    this.Hide();
                }
                else
                {
                    string errorMessage;

                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        errorMessage = await result.Content
                            .ReadAsStringAsync();
                    }
                    else
                    {
                        errorMessage = $"{result.StatusCode} - {result.ReasonPhrase}";
                    }

                    MessageBox.Show($"Ошибка: {errorMessage}");
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

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            RoleService roleService = new RoleService(new HttpClientFactory());
            RoleListWindow window = new RoleListWindow();
            window.InjectTaskTypeService(roleService);
            window.Show();
            this.Hide();
        }
    }
}
