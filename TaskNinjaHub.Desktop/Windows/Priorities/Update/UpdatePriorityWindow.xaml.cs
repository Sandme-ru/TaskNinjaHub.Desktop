using System.Windows;
using TaskNinjaHub.Desktop.Models.Priorities;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Priorities.List;

namespace TaskNinjaHub.Desktop.Windows.Priorities.Update;

/// <summary>
/// Логика взаимодействия для UpdatePriorityWindow.xaml
/// </summary>
public partial class UpdatePriorityWindow : Window
{
    private PriorityService _priorityService;

    private Priority _priority;

    public UpdatePriorityWindow()
    {
        InitializeComponent();
        _priority = PriorityListWindow.Priority;
        NameBox.Text = _priority.Name;
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
            _priority.Name = NameBox.Text;

            var result = await _priorityService.UpdateAsync(_priority);

            if (result.Success)
            {
                MessageBox.Show("Приоритет задачи успешно обновлен");
                PriorityService priorityService = new PriorityService(new HttpClientFactory());
                PriorityListWindow window = new PriorityListWindow();
                window.InjectTaskTypeService(priorityService);
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
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        PriorityListWindow window = new PriorityListWindow();
        window.InjectTaskTypeService(priorityService);
        window.Show();
        this.Hide();
    }

    private void CancleButton_Click(object sender, RoutedEventArgs e)
    {
        PriorityService priorityService = new PriorityService(new HttpClientFactory());
        PriorityListWindow window = new PriorityListWindow();
        window.InjectTaskTypeService(priorityService);
        window.Show();
        this.Hide();
    }
}