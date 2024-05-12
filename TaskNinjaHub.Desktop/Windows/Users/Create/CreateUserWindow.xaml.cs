using System.Windows;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Users.List;

namespace TaskNinjaHub.Desktop.Windows.Users.Create;

/// <summary>
/// Логика взаимодействия для CreateUserWindow.xaml
/// </summary>
public partial class CreateUserWindow : Window
{

    private UserService _userService;

    private RoleService _roleService;

    public CreateUserWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
    }
    public async Task PopulateRoleComboBox()
    {
        var roles = await GetAllTypes();
        foreach (var role in roles)
        {
            RoleComboBox.Items.Add(role.Name);
        }
    }
    private async Task<IEnumerable<Role>> GetAllTypes()
    {
        return await _roleService.GetAllAsync();
    }

    public void InjectTaskTypeService(UserService userService, RoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameBox.Text.Length > 0)
        {
            UserDto userDto = new UserDto()
            {
                Email = EmailBox.Text,
                UserName = EmailBox.Text,
                FirstName = NameBox.Text,
                LastName = SurnameBox.Text,
                MiddleName = MiddleBox.Text,
                PhoneNumber = PhoneBox.Text,
                Password = PasswordBox.Text,
                Role = RoleComboBox.SelectedItem.ToString(),
            };

            var result = await _userService.AddUserAsync(userDto);

            if (result.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь успешно добавлен");
                UserListWindow roleListWindow = new();
                UserService userService = new UserService(new HttpClientFactory());
                roleListWindow.InjectTaskTypeService(userService);
                this.Hide();
                roleListWindow.Show();
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
}