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
                MessageBox.Show("Приоритет задачи успешно добавлен");
                var priorityService = new PriorityService(new HttpClientFactory());
                var window = new PriorityListWindow();
                window.InjectPriorityService(priorityService);
                window.Show();
                this.Hide();
            }
        }
        else
        {
            MessageBox.Show("Введите название для приоритета задачи");
        }
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        var priorityService = new PriorityService(new HttpClientFactory());
        var window = new PriorityListWindow();
        window.InjectPriorityService(priorityService);
        window.Show();
        this.Hide();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        var priorityService = new PriorityService(new HttpClientFactory());
        var window = new PriorityListWindow();
        window.InjectPriorityService(priorityService);
        window.Show();
        this.Hide();
    }
}