using System.Windows;
using System.Windows.Controls;
using TaskNinjaHub.Desktop.Models.TaskTypes;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Menu;
using TaskNinjaHub.Desktop.Windows.Types.Create;
using TaskNinjaHub.Desktop.Windows.Types.Update;

namespace TaskNinjaHub.Desktop.Windows.Types.List
{
    /// <summary>
    /// Логика взаимодействия для TypeListWindow.xaml
    /// </summary>
    public partial class TypeListWindow : Window
    {
        private TaskTypeService _taskTypeService;

        public static CatalogTaskType CatalogTaskType = null!;

        public TypeListWindow()
        {
            InitializeComponent();
            NameTextBlock.Text = PropertyStorage.Username;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private async Task InitializeAsync()
        {
            if (_taskTypeService != null)
            {
                var typesList = await GetAllTypes();
                TypesDataGrid.ItemsSource = typesList;
            }
            else
            {
                MessageBox.Show("Ошибка: объект _taskTypeService не был инициализирован.");
            }
        }

        private async Task<IEnumerable<CatalogTaskType>> GetAllTypes()
        {
            return await _taskTypeService.GetAllAsync();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        public void InjectTaskTypeService(TaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesDataGrid.SelectedItems.Count == 1)
            {
                CatalogTaskType = (CatalogTaskType)TypesDataGrid.SelectedItem;
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
            CreateTypeWindow createTypeWindow = new CreateTypeWindow();
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            createTypeWindow.InjectTaskTypeService(taskTypeService);
            this.Hide();
            createTypeWindow.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Hide();
            mainMenuWindow.Show();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CatalogTaskType != null!)
            {
                await _taskTypeService.DeleteAsync(CatalogTaskType.Id);
            }
            else
            {
                MessageBox.Show("Выберите строку, которую хотите удалить");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTypeWindow updateTypeWindow = new UpdateTypeWindow();
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            updateTypeWindow.InjectTaskTypeService(taskTypeService);
            this.Hide();
            updateTypeWindow.Show();
        }
    }
}