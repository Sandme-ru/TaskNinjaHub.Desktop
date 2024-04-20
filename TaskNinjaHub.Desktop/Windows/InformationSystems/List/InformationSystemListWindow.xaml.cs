using System.Windows;
using System.Windows.Controls;
using TaskNinjaHub.Desktop.Models.InformationSystems;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.Create;
using TaskNinjaHub.Desktop.Windows.InformationSystems.Update;
using TaskNinjaHub.Desktop.Windows.Menu;

namespace TaskNinjaHub.Desktop.Windows.InformationSystems.List;

/// <summary>
/// Логика взаимодействия для InformationSystemListWindow.xaml
/// </summary>
public partial class InformationSystemListWindow : Window
{
    private InformationSystemService _informationSystemService;

    public static InformationSystem InformationSystem = null!;

    public InformationSystemListWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
        UpdateButton.IsEnabled = false;
        DeleteButton.IsEnabled = false;
    }
    private async Task InitializeAsync()
    {
        if (_informationSystemService != null)
        {
            var typesList = await GetAllTypes();
            InformationSystemDataGrid.ItemsSource = typesList;
        }
        else
        {
            MessageBox.Show("Ошибка: объект _priorityService не был инициализирован.");
        }
    }

    private async Task<IEnumerable<Models.InformationSystems.InformationSystem>> GetAllTypes()
    {
        return await _informationSystemService.GetAllAsync();
    }

    private async void Window_Loaded_1(object sender, RoutedEventArgs e)
    {
        await InitializeAsync();
    }

    public void InjectTaskTypeService(InformationSystemService informationSystemService)
    {
        _informationSystemService = informationSystemService;
    }

    private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (InformationSystemDataGrid.SelectedItems.Count == 1)
        {
            InformationSystem = (InformationSystem)InformationSystemDataGrid.SelectedItem;
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
        CreateInformationSystemWindow createInformationSystemWindow = new CreateInformationSystemWindow();
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        createInformationSystemWindow.InjectTaskTypeService(informationSystemService);
        this.Hide();
        createInformationSystemWindow.Show();
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        MainMenuWindow mainMenuWindow = new MainMenuWindow();
        this.Hide();
        mainMenuWindow.Show();
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (InformationSystem != null!)
        {
            await _informationSystemService.DeleteAsync(InformationSystem.Id);
            var typesList = await GetAllTypes();
            InformationSystemDataGrid.ItemsSource = typesList;

            MessageBox.Show("Информационная система успешно удалена");
        }
        else
        {
            MessageBox.Show("Выберите строку, которую хотите удалить");
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateInformationSystemWindow updateInformationSystemWindow = new UpdateInformationSystemWindow();
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        updateInformationSystemWindow.InjectTaskTypeService(informationSystemService);
        this.Hide();
        updateInformationSystemWindow.Show();
    }
}