using System.Windows;
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
            UserListWindow userListWindow = new();
            this.Hide();
            userListWindow.Show();
        }

        private void rolesButton_Click(object sender, RoutedEventArgs e)
        {
            RoleListWindow roleListWindow = new();
            this.Hide();
            roleListWindow.Show();
        }

        private void informationSystemsButton_Click(object sender, RoutedEventArgs e)
        {
            InformationSystemListWindow informationSystemListWindow = new();
            this.Hide();
            informationSystemListWindow.Show();
        }

        private void typesButton_Click(object sender, RoutedEventArgs e)
        {
            TypeListWindow typeListWindow = new();
            this.Hide();
            typeListWindow.Show();
        }

        private void prioritiesButton_Click(object sender, RoutedEventArgs e)
        {
            PriorityListWindow prioritiesListWindow = new();
            this.Hide();
            prioritiesListWindow.Show();
        }
    }
}
