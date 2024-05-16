using System.Net;
using System.Windows;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Windows.Users.List;

namespace TaskNinjaHub.Desktop.Windows.Users.Update
{
    /// <summary>
    /// Логика взаимодействия для UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        private UserService _userService;

        private RoleService _roleService;

        private ApplicationUser _applicationUser;

        private string Role;

        public UpdateUserWindow()
        {
            InitializeComponent();
            _applicationUser = UserListWindow.ApplicationUser;
            EmailBox.Text = _applicationUser.Email;
            NameBox.Text = _applicationUser.FirstName;
            SurnameBox.Text = _applicationUser.LastName;
            MiddleBox.Text = _applicationUser.MiddleName;
            PhoneBox.Text = _applicationUser.PhoneNumber;

            PasswordBox.IsEnabled = false;
        }

        public async Task PopulateRoleComboBox()
        {
            var roles = await GetAllTypes();
            foreach (var role in roles)
            {
                RoleComboBox.Items.Add(role.Name);
            }
            RoleComboBox.SelectedItem = Role;
        }
        private async Task<IEnumerable<Role>> GetAllTypes()
        {
            Role = await _roleService.GetUserRoleAsync(_applicationUser.Id);
            return await _roleService.GetAllAsync();
        }

        public void InjectTaskTypeService(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Length > 0 && SurnameBox.Text.Length > 0 && NameBox.Text.Length > 0 && MiddleBox.Text.Length > 0 && PhoneBox.Text.Length > 0 && RoleComboBox.Text.Length > 0)
            {
                UserDto userDto = new UserDto()
                {
                    Id = _applicationUser.Id,
                    Email = EmailBox.Text,
                    UserName = EmailBox.Text,
                    FirstName = NameBox.Text,
                    LastName = SurnameBox.Text,
                    MiddleName = MiddleBox.Text,
                    PhoneNumber = PhoneBox.Text,
                    Role = RoleComboBox.SelectedItem.ToString(),
                };

                var exisiRoles = await _userService.GetAllAsync();
                if (exisiRoles.Any(x => x.UserName == EmailBox.Text))
                {
                    MessageBox.Show("Такой пользователь уже существует");
                    return;
                }

                if (PasswordBox.IsEnabled = true)
                {
                    if (PasswordBox.Text.Length > 0)
                        userDto.Password = PasswordBox.Text;
                    else
                    {
                        MessageBox.Show("Введите пароль");
                        return;
                    }
                }


                var result = await _userService.EditUserAsync(userDto);

                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пользователь успешно обновлен");
                    UserListWindow roleListWindow = new();
                    UserService userService = new UserService(new HttpClientFactory());
                    roleListWindow.InjectTaskTypeService(userService);
                    this.Hide();
                    roleListWindow.Show();
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
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow roleListWindow = new();
            UserService userService = new UserService(new HttpClientFactory());
            roleListWindow.InjectTaskTypeService(userService);
            this.Hide();
            roleListWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow roleListWindow = new();
            UserService userService = new UserService(new HttpClientFactory());
            roleListWindow.InjectTaskTypeService(userService);
            this.Hide();
            roleListWindow.Show();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateRoleComboBox();
        }

        private void PasswordCheck_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBox.IsEnabled = true;
        }

        private void PasswordCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.IsEnabled = false;
        }
    }
}
