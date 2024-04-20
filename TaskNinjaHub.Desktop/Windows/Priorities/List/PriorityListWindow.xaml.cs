using System.Windows;
using System.Windows.Controls;
using TaskNinjaHub.Desktop.Models.Priorities;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Menu;
using TaskNinjaHub.Desktop.Windows.Priorities.Create;
using TaskNinjaHub.Desktop.Windows.Priorities.Update;

namespace TaskNinjaHub.Desktop.Windows.Priorities.List;

/// <summary>
/// Логика взаимодействия для PriorityListWindow.xaml
/// </summary>
public partial class PriorityListWindow : Window
{
    private PriorityService _priorityService;

    public static Priority Priority = null!;

    public PriorityListWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
        UpdateButton.IsEnabled = false;
        DeleteButton.IsEnabled = false;
    }
    private async Task InitializeAsync()
    {
        if (_priorityService != null)
        {
            var typesList = await GetAllTypes();
            PriorityDataGrid.ItemsSource = typesList;
        }
        else
        {
            MessageBox.Show("Ошибка: объект _priorityService не был инициализирован.");
        }
    }

    private async Task<IEnumerable<Models.Priorities.Priority>> GetAllTypes()
    {
        return await _priorityService.GetAllAsync();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await InitializeAsync();
    }

    public void InjectTaskTypeService(PriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PriorityDataGrid.SelectedItems.Count == 1)
        {
            Priority = (Priority)PriorityDataGrid.SelectedItem;
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
        CreatePriorityWindow createPriorityWindow = new CreatePriorityWindow();
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        createPriorityWindow.InjectTaskTypeService(priorityService);
        this.Hide();
        createPriorityWindow.Show();
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        MainMenuWindow mainMenuWindow = new MainMenuWindow();
        this.Hide();
        mainMenuWindow.Show();
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (Priority != null!)
        {
            await _priorityService.DeleteAsync(Priority.Id);
            var typesList = await GetAllTypes();
            PriorityDataGrid.ItemsSource = typesList;

            MessageBox.Show("Приоритет успешно удален");
        }
        else
        {
            MessageBox.Show("Выберите строку, которую хотите удалить");
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        UpdatePriorityWindow priorityWindow = new UpdatePriorityWindow();
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        priorityWindow.InjectTaskTypeService(priorityService);
        this.Hide();
        priorityWindow.Show();
    }
}