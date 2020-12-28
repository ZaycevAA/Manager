using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для Download.xaml
    /// </summary>
    public partial class Download : Window
    {
        private ObservableCollection<Program> _data;
        private List<Program> _quality;


        
        private int quan;
        
        public Download(object prog , bool ifer)
        {
            InitializeComponent();
            if (ifer== true)
            {
                _quality = new List<Program> { prog as Program };
                quan = 1;
            }
            else
            {
                _data = prog as ObservableCollection<Program>;
                
            }
            
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WebClient Client = new WebClient())
                {
                    string response;
                    response = Client.DownloadString("https://ya.ru");
                }
            }
            catch (Exception)
            {
                DialogResult = true;
                Close();
                MessageBox.Show("Проверте продключение к интернету");
            }




            if (_quality == null)
            {
                _quality = new List<Program>();
                quan = 0;
                Directory.CreateDirectory($@"{Environment.CurrentDirectory}\Folder");
                foreach (var item in _data)
                {
                    if ((!File.Exists($@"{Environment.CurrentDirectory}\Folder\{item.Name}{GetFileExtension(item)}")) && (item.Check==true))
                    {
                        quan += 1;
                        _quality.Insert(0, item);
                    }
                }
            }

            
            if (quan == 0)
            {
                DialogResult = true;
                Close();
                MessageBox.Show("Программы уже скачены или не выброны");
            }

            if ((_quality != null) && (quan != 0))
            {
                quanBar.Maximum = _quality.Count;

                foreach (var item in _quality)
                {
                    try
                    {
                        await Downloader(item);
                        if (quanBar.Maximum == quanBar.Value)
                        {

                            DialogResult = true;
                            Close();
                            MessageBox.Show("Загрузка завершена");

                        }
                    }
                    catch (Exception)
                    {
                        
                        Close();
                        MessageBox.Show("Допишите расширение");
                    }
                }


            }
            
            Close();
        }
        private string GetFileExtension(Program fileName)
        {
            if (fileName.Name.Contains("."))
            {
                return null;
            }
            else
            {
                return fileName.Path.Substring(fileName.Path.LastIndexOf("."));
            }
        }

        private Task Downloader(Program item)
        {
            using (var web = new WebClient())
            {

                progBar.Value = 0;

                web.OpenRead(item.Path);

                web.DownloadProgressChanged += (c, t) =>
                {
                    quantity.Content = item.Name;
                    prog.Content = $"{item.Size}: " +
                    $"Загружено {t.ProgressPercentage}% " +
                    $"({(t.BytesReceived / 1024).ToString("# КБ")})";
                    progBar.Value = t.ProgressPercentage;
                };
                web.DownloadFileCompleted += (c, t) =>
                {
                    quanBar.Value++;
                };

                var task = web.DownloadFileTaskAsync(
                                    new Uri(item.Path),
                                  $@"{Environment.CurrentDirectory}\Folder\" +
                                  $@"{item.Name}{GetFileExtension(item)}");
                return task;

            }
        }
    }
}
