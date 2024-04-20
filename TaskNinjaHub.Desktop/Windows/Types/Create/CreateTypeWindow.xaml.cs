using System.Windows;
using TaskNinjaHub.Desktop.Services.HttpClientServices;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.Storages;
using TaskNinjaHub.Desktop.Windows.Types.List;
using CatalogTaskType = TaskNinjaHub.Desktop.Models.TaskTypes.CatalogTaskType;

namespace TaskNinjaHub.Desktop.Windows.Types.Create
{
    /// <summary>
    /// Логика взаимодействия для CreateTypeWindow.xaml
    /// </summary>
    public partial class CreateTypeWindow : Window
    {
        private TaskTypeService _taskTypeService;

        public CreateTypeWindow()
        {
            InitializeComponent();
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
                Models.TaskTypes.CatalogTaskType catalogTaskType = new CatalogTaskType()
                {
                    Name = NameBox.Text
                };

                var result = await _taskTypeService.CreateAsync(catalogTaskType);

                if (result.Success)
                {
                    MessageBox.Show("Тип задачиуспешно добавлен");
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            TaskTypeService taskTypeService = new TaskTypeService(new HttpClientFactory());
            TypeListWindow window = new TypeListWindow();
            window.InjectTaskTypeService(taskTypeService);
            window.Show();
            this.Hide();
        }
    }
}
