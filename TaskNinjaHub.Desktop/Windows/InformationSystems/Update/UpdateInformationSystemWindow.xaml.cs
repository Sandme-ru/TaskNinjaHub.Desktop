using System.Windows;
using TaskNinjaHub.Desktop.Models.InformationSystems;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.InformationSystems.List;

namespace TaskNinjaHub.Desktop.Windows.InformationSystems.Update;

/// <summary>
/// Логика взаимодействия для UpdateInformationSystemWindow.xaml
/// </summary>
public partial class UpdateInformationSystemWindow : Window
{
    private InformationSystemService _systemService;

    private InformationSystem _informationSystem;

    public UpdateInformationSystemWindow()
    {
        InitializeComponent();
        _informationSystem = InformationSystemListWindow.InformationSystem;
        NameBox.Text = _informationSystem.Name;
        NameTextBlock.Text = PropertyStorage.Username;
    }
    public void InjectTaskTypeService(InformationSystemService informationSystem)
    {
        _systemService = informationSystem;
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        if (NameBox.Text.Length > 0)
        {
            _informationSystem.Name = NameBox.Text;

            var result = await _systemService.UpdateAsync(_informationSystem);

            if (result.Success)
            {
                MessageBox.Show("Информационная система успешно обновлена");
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

    private void CancleButton_Click(object sender, RoutedEventArgs e)
    {
        InformationSystemService informationSystemService = new InformationSystemService(new HttpClientFactory());
        InformationSystemListWindow window = new InformationSystemListWindow();
        window.InjectTaskTypeService(informationSystemService);
        window.Show();
        this.Hide();
    }
}