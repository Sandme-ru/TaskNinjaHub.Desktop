using System.Windows;
using TaskNinjaHub.Desktop.Models.Priorities;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Priorities.List;
using TaskNinjaHub.Desktop.Windows.Types.List;

namespace TaskNinjaHub.Desktop.Windows.Priorities.Create;

/// <summary>
/// Логика взаимодействия для CreatePriorityWindow.xaml
/// </summary>
public partial class CreatePriorityWindow : Window
{
    private PriorityService _priorityService;
    public CreatePriorityWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
    }

    public void InjectTaskTypeService(PriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameBox.Text.Length > 0)
        {
            Models.Priorities.Priority catalogTaskType = new Priority()
            {
                Name = NameBox.Text
            };

            var result = await _priorityService.CreateAsync(catalogTaskType);

            if (result.Success)
            {
                MessageBox.Show("Тип задачиуспешно добавлен");
                PriorityService priorityService = new PriorityService(new HttpClientFactory());
                PriorityListWindow window = new PriorityListWindow();
                window.InjectTaskTypeService(priorityService);
                window.Show();
                this.Hide();
            }
        }
        else
        {
            MessageBox.Show("Введите название для типа задачи");
        }
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        PriorityListWindow window = new PriorityListWindow();
        window.InjectTaskTypeService(priorityService);
        window.Show();
        this.Hide();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        PriorityListWindow window = new PriorityListWindow();
        window.InjectTaskTypeService(priorityService);
        window.Show();
        this.Hide();
    }
}