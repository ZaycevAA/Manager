using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ProgramsView _data = new ProgramsView();
        public MainWindow()
        {

            InitializeComponent();
            DataContext = _data;
        }

        private void Manager_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {


            e.Cancel = true;

            MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Выйти?", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {

                _data.SaveCommand.Execute(sender);
                Environment.Exit(1);

            }
            else if (result == MessageBoxResult.No)
            {
                Environment.Exit(1);
            };
        }

        private void Manager_Activated(object sender, EventArgs e)
        {
            _data.CheckCommand.Execute(sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uplist();
            if (bChoise.Content == "Снять выбор")
            {
                bChoise.Content = "Выбрать все";
            }
            else
            {
                bChoise.Content = "Снять выбор";
            }

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Uplist();
        }

        private void Uplist()
        {
            listbox.DataContext = null;
            listbox.DataContext = _data;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{Environment.CurrentDirectory}\\Folder\\");
        }
    }
}
