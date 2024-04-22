using System.Windows;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Services.UserServices.Roles;
using TaskNinjaHub.Desktop.Services.UserServices.Users;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.List;
using TaskNinjaHub.Desktop.Windows.Priorities.List;
using TaskNinjaHub.Desktop.Windows.Priorities.Update;
using TaskNinjaHub.Desktop.Windows.Roles.List;
using TaskNinjaHub.Desktop.Windows.Types.List;
using TaskNinjaHub.Desktop.Windows.Users.List;

namespace TaskNinjaHub.Desktop.Windows.Menu
{
    /// <summary>
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();

            NameTextBlock.Text = PropertyStorage.Username;
        }

        private void usersButton_Click(object sender, RoutedEventArgs e)
        {
            UserListWindow roleListWindow = new();
            UserService userService = new UserService(new HttpClientFactory());
            roleListWindow.InjectTaskTypeService(userService);
            this.Hide();
            roleListWindow.Show();
        }

        private void rolesButton_Click(object sender, RoutedEventArgs e)
        {
            RoleListWindow roleListWindow = new();
            RoleService roleService = new RoleService(new HttpClientFactory());
            roleListWindow.InjectTaskTypeService(roleService);
            this.Hide();
            roleListWindow.Show();
        }

        private void informationSystemsButton_Click(object sender, RoutedEventArgs e)
        {
            InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
            InformationSystemListWindow informationSystemListWindow = new InformationSystemListWindow();
            informationSystemListWindow.InjectTaskTypeService(informationSystemService);
            this.Hide();
            informationSystemListWindow.Show();
        }

        private void typesButton_Click(object sender, RoutedEventArgs e)
        {
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            TypeListWindow window = new TypeListWindow();
            window.InjectTaskTypeService(taskTypeService);
            window.Show();
            this.Hide();
        }

        private void prioritiesButton_Click(object sender, RoutedEventArgs e)
        {
            PriorityListWindow prioritiesListWindow = new PriorityListWindow();
            PriorityService priorityService = new PriorityService(new HttpClientFactory());
            prioritiesListWindow.InjectTaskTypeService(priorityService);
            this.Hide();
            prioritiesListWindow.Show();
        }
    }
}
