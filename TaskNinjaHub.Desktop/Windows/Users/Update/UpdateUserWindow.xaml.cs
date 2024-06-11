using System.Net;
using System.Windows;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Users.List;

namespace TaskNinjaHub.Desktop.Windows.Users.Update;

/// <summary>
/// Логика взаимодействия для UpdateUserWindow.xaml
/// </summary>
public partial class UpdateUserWindow : Window
{
    private UserService _userService;

    private RoleService _roleService;

    private readonly ApplicationUser _applicationUser;

    private string _role;

    public UpdateUserWindow()
    {
        InitializeComponent();
        _applicationUser = UserListWindow.ApplicationUser;

        EmailBox.Text = _applicationUser.Email ?? string.Empty;
        NameBox.Text = _applicationUser.FirstName ?? string.Empty;
        SurnameBox.Text = _applicationUser.LastName ?? string.Empty;
        MiddleBox.Text = _applicationUser.MiddleName ?? string.Empty;
        PhoneBox.Text = _applicationUser.PhoneNumber ?? string.Empty;

        PasswordBox.IsEnabled = false;
    }

    public async Task PopulateRoleComboBox()
    {
        var roles = await GetAllTypes();

        foreach (var role in roles)
            RoleComboBox.Items.Add(role.Name);

        RoleComboBox.SelectedItem = _role;
    }

    private async Task<IEnumerable<Role>> GetAllTypes()
    {
        _role = await _roleService.GetUserRoleAsync(_applicationUser.Id);
        return await _roleService.GetAllAsync();
    }

    public void InjectTaskTypeService(UserService userService, RoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        await UpdateUser();
    }

    private async Task UpdateUser()
    {
        if (EmailBox.Text.Length > 0 && SurnameBox.Text.Length > 0 && NameBox.Text.Length > 0 && MiddleBox.Text.Length > 0 && PhoneBox.Text.Length > 0 && RoleComboBox.Text.Length > 0)
        {
            var userDto = new UserDto()
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
                var exitName = exisiRoles.FirstOrDefault(x => x.UserName == EmailBox.Text);
                if (exitName != null)
                {
                    if (exitName.UserName != _applicationUser.UserName)
                    {
                        MessageBox.Show("Такой пользователь уже существует");
                        return;
                    }
                }

                if (PasswordBox.IsEnabled)
                {
                    case < 8:
                        MessageBox.Show("Пароль должен быть от 8 символов");
                        return;
                    case > 0:
                        userDto.Password = PasswordBox.Password;
                        break;
                    default:
                        MessageBox.Show("Вы не заполнили поле Пароль (пароль должен быть от 8 символов)");
                        return;
                }
            }
                
            var result = await _userService.EditUserAsync(userDto);

            if (result.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь успешно обновлен");

                var roleListWindow = new UserListWindow();
                var userService = new UserService(new HttpClientFactory());

                roleListWindow.InjectUserService(userService);
                this.Hide();
                roleListWindow.Show();
            }
            else
            {
                string errorMessage;

                if (result.StatusCode == HttpStatusCode.BadRequest)
                    errorMessage = await result.Content.ReadAsStringAsync();
                else
                    errorMessage = $"{result.StatusCode} - {result.ReasonPhrase}";

                MessageBox.Show($"Ошибка: {errorMessage}");
            }
        }
        else
            MessageBox.Show("Заполните все поля!");
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        NavigateBack();
    }

    private void NavigateBack()
    {
        var userListWindow = new UserListWindow();
        var userService = new UserService(new HttpClientFactory());
        userListWindow.InjectUserService(userService);
        this.Hide();
        userListWindow.Show();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        NavigateBack();
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