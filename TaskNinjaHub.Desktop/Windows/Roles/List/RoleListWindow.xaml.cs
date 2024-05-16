using System.Windows;
using System.Windows.Controls;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Menu;
using TaskNinjaHub.Desktop.Windows.Priorities.Create;
using TaskNinjaHub.Desktop.Windows.Priorities.Update;
using TaskNinjaHub.Desktop.Windows.Roles.Create;
using TaskNinjaHub.Desktop.Windows.Roles.Update;

namespace TaskNinjaHub.Desktop.Windows.Roles.List
{
    /// <summary>
    /// Логика взаимодействия для RoleListWindow.xaml
    /// </summary>
    public partial class RoleListWindow : Window
    {
        private RoleService _roleService;

        public static Role Role = null!;

        public RoleListWindow()
        {
            InitializeComponent();
            NameTextBlock.Text = PropertyStorage.Username;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }
        private async Task InitializeAsync()
        {
            if (_roleService != null!)
            {
                var typesList = await GetAllTypes();
                InformationSystemDataGrid.ItemsSource = typesList;
            }
            else
            {
                MessageBox.Show("Ошибка: объект _priorityService не был инициализирован.");
            }
        }

        private async Task<IEnumerable<Role>> GetAllTypes()
        {
            return await _roleService.GetAllAsync();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        public void InjectTaskTypeService(RoleService roleService)
        {
            _roleService = roleService;
        }

        private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InformationSystemDataGrid.SelectedItems.Count == 1)
            {
                Role = (Role)InformationSystemDataGrid.SelectedItem;
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
            CreateRoleWindow createRoleWindow = new CreateRoleWindow();
            RoleService roleService = new RoleService(new HttpClientFactory());
            createRoleWindow.InjectTaskTypeService(roleService);
            this.Hide();
            createRoleWindow.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Hide();
            mainMenuWindow.Show();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Role != null!)
            {
                await _roleService.DeleteRoleAsync(Role.Id.ToString());
                var typesList = await GetAllTypes();
                InformationSystemDataGrid.ItemsSource = typesList;

                MessageBox.Show("Роль успешно удалена");
            }
            else
            {
                MessageBox.Show("Выберите строку, которую хотите удалить");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRoleWindow updateRoleWindow = new UpdateRoleWindow();
            RoleService roleService = new RoleService(new HttpClientFactory());
            updateRoleWindow.InjectTaskTypeService(roleService);
            this.Hide();
            updateRoleWindow.Show();
        }
    }
}
