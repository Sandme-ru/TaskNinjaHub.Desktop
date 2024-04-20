using System.Windows;
using TaskNinjaHub.Desktop.Models.InformationSystems;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.List;
using TaskNinjaHub.Desktop.Windows.Priorities.List;

namespace TaskNinjaHub.Desktop.Windows.InformationSystems.Create;

/// <summary>
/// Логика взаимодействия для CreateInformationSystemWindow.xaml
/// </summary>
public partial class CreateInformationSystemWindow : Window
{
    private InformationSystemService _informationSystemService;

    public CreateInformationSystemWindow()
    {
        InitializeComponent();
        NameTextBlock.Text = PropertyStorage.Username;
    }

    public void InjectTaskTypeService(InformationSystemService informationSystemService)
    {
        _informationSystemService = informationSystemService;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameBox.Text.Length > 0)
        {
            InformationSystem informationSystem = new InformationSystem()
            {
                Name = NameBox.Text
            };

            var result = await _informationSystemService.CreateAsync(informationSystem);

            if (result.Success)
            {
                MessageBox.Show("Информационная система успешно добавлен");
                InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
                InformationSystemListWindow window = new InformationSystemListWindow();
                window.InjectTaskTypeService(informationSystemService);
                window.Show();
                this.Hide();
            }
        }
        else
        {
            MessageBox.Show("Введите название для информационной системы");
        }
    }

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        InformationSystemListWindow window = new InformationSystemListWindow();
        window.InjectTaskTypeService(informationSystemService);
        window.Show();
        this.Hide();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        InformationSystemListWindow window = new InformationSystemListWindow();
        window.InjectTaskTypeService(informationSystemService);
        window.Show();
        this.Hide();
    }
}