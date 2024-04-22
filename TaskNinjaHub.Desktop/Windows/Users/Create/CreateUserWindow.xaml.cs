using System.Windows;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.List;

namespace TaskNinjaHub.Desktop.Windows.Users.Create;

/// <summary>
/// Логика взаимодействия для CreateUserWindow.xaml
/// </summary>
public partial class CreateUserWindow : Window
{

    private UserService _userService;
    public CreateUserWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
    }

    public void InjectTaskTypeService(UserService userService)
    {
        _userService = userService;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameBox.Text.Length > 0)
        {
            UserDto userDto = new UserDto()
            {
                Email = NameBox.Text,
                FirstName = NameBox.Text,
                LastName = NameBox.Text,
                MiddleName = NameBox.Text,
                PhoneNumber = NameBox.Text,
            };

            var result = await _userService.AddUserAsync(userDto);

            if (result.IsSuccessStatusCode)
            {
                MessageBox.Show("Пользователь успешно добавлен");
                InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
                InformationSystemListWindow window = new InformationSystemListWindow();
                window.InjectTaskTypeService(informationSystemService);
                window.Show();
                this.Hide();
            }
        }
        else
        {
            MessageBox.Show("Заполните все поля!");
        }
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        InformationSystemListWindow window = new InformationSystemListWindow();
        window.InjectTaskTypeService(informationSystemService);
        window.Show();
        this.Hide();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        InformationSystemListWindow window = new InformationSystemListWindow();
        window.InjectTaskTypeService(informationSystemService);
        window.Show();
        this.Hide();
    }
}