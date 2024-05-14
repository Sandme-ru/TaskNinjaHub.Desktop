using System.Windows;
using System.Windows.Controls;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.Create;
using TaskNinjaHub.Desktop.Windows.InformationSystems.Update;
using TaskNinjaHub.Desktop.Windows.Menu;
using TaskNinjaHub.Desktop.Windows.Users.Create;
using TaskNinjaHub.Desktop.Windows.Users.Update;

namespace TaskNinjaHub.Desktop.Windows.Users.List
{
    /// <summary>
    /// Логика взаимодействия для UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        private UserService _userService;

        public static ApplicationUser ApplicationUser = null!;
        public UserListWindow()
        {
            InitializeComponent();
            NameTextBlock.Text = PropertyStorage.Username;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }
        private async Task InitializeAsync()
        {
            if (_userService != null)
            {
                var typesList = await GetAllTypes();
                InformationSystemDataGrid.ItemsSource = typesList;
            }
            else
            {
                MessageBox.Show("Ошибка: объект _priorityService не был инициализирован.");
            }
        }

        private async Task<IEnumerable<ApplicationUser>> GetAllTypes()
        {
            return await _userService.GetAllAsync();
        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        public void InjectTaskTypeService(UserService userService)
        {
            _userService = userService;
        }

        private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InformationSystemDataGrid.SelectedItems.Count == 1)
            {
                ApplicationUser = (ApplicationUser)InformationSystemDataGrid.SelectedItem;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            else
            {
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWindow = new();
            UserService userService = new UserService(new HttpClientFactory());
            RoleService roleService = new RoleService(new HttpClientFactory());
            createUserWindow.InjectTaskTypeService(userService, roleService);
            this.Hide();
            createUserWindow.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Hide();
            mainMenuWindow.Show();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationUser != null!)
            {
                await _userService.DeleteUserAsync(ApplicationUser.Id);
                var typesList = await GetAllTypes();
                InformationSystemDataGrid.ItemsSource = typesList;

                MessageBox.Show("Пользователь успешно удален");
            }
            else
            {
                MessageBox.Show("Выберите строку, которую хотите удалить");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateUserWindow updateUserWindow = new();
            UserService userService = new UserService(new HttpClientFactory());
            RoleService roleService = new RoleService(new HttpClientFactory());
            updateUserWindow.InjectTaskTypeService(userService, roleService);
            this.Hide();
            updateUserWindow.Show();
        }
    }
}
