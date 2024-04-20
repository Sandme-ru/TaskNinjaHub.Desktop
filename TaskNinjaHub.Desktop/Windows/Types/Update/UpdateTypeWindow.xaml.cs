using System.Windows;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Types.List;
using CatalogTaskType = TaskNinjaHub.Desktop.Models.TaskTypes.CatalogTaskType;

namespace TaskNinjaHub.Desktop.Windows.Types.Update
{
    /// <summary>
    /// Логика взаимодействия для UpdateTypeWindow.xaml
    /// </summary>
    public partial class UpdateTypeWindow : Window
    {

        private TaskTypeService _taskTypeService;

        private CatalogTaskType _catalogTaskType;
        public UpdateTypeWindow()
        {
            InitializeComponent();
            _catalogTaskType = TypeListWindow.CatalogTaskType;
            NameBox.Text = _catalogTaskType.Name;
            NameTextBlock.Text = PropertyStorage.Username;
        }

        public void InjectTaskTypeService(TaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length > 0)
            {
                _catalogTaskType.Name = NameBox.Text;

                var result = await _taskTypeService.UpdateAsync(_catalogTaskType);

                if (result.Success)
                {
                    MessageBox.Show("Тип задачи успешно обновлен");
                    TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
                    TypeListWindow window = new TypeListWindow();
                    window.InjectTaskTypeService(taskTypeService);
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
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            TypeListWindow window = new TypeListWindow();
            window.InjectTaskTypeService(taskTypeService);
            window.Show();
            this.Hide();
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            TypeListWindow window = new TypeListWindow();
            window.InjectTaskTypeService(taskTypeService);
            window.Show();
            this.Hide();
        }
    }
}
