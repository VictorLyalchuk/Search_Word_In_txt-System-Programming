using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Path = System.IO.Path;

namespace Exam
{
    public partial class MainWindow : Window
    {
        ViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                Source = "",
                Word = "",
            };
            DataContext = model;
        }
        private void Button_Click_Browse_Folder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                model.Source = dialog.FileName;
            }
        }
        private async void Button_Click_Search_ForAsync(object sender, RoutedEventArgs e)
        {
            if (model.Source != "" && model.Word != "")
            {
                string[] pathes = Directory.GetFiles(model.Source, "*.txt", SearchOption.AllDirectories);
                foreach (string path in pathes)
                {
                    SearchForInfo info = new SearchForInfo();
                    model.AddProgress(info);
                    await SearchFor(path, info);
                }
                MessageBox.Show("Complited");
                SaveButton.IsEnabled = true;
            }
            else MessageBox.Show("Sorry, I'm busy!!!");
        }
        private Task SearchFor(string path, SearchForInfo info)
        {
            return Task.Run(() =>
            {
                int count = 0;
                string text = File.ReadAllText(path);
                string[] words = text.Split(new char[] { ' ', ',', '.', '-', ':', '!', '?', '"', '<', '>', '(', ')', '{', '}', '/', '-', '\n' });
                foreach (string word in words)
                {
                    if (word == model.Word) count++;
                }
                info.FileName = Path.GetFileName(path);
                info.Path = path;
                info.CountWord = count;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.ClearProgress();
            SaveButton.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog2 = new CommonOpenFileDialog();
            dialog2.IsFolderPicker = true;
            string path ="";
            if (dialog2.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog2.FileName;
            }
            Save(path);
        }
        private Task Save(string path)
        {
            return Task.Run(() =>
            {
                using FileStream createStream = File.Create($@"{path}"+$@"\Processes.json");
                JsonSerializer.SerializeAsync(createStream, model);
            });
        }
    }
}
