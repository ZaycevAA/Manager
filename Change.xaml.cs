using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Change.xaml
    /// </summary>
    public partial class Change : Window
    {

        public Change()
        {
            InitializeComponent();

        }
        private long _size;
        public long ProgSize
        {
            get { return _size; }
            set { _size = value; }
        }
        public string NameProgram
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Path
        {
            get { return tbPath.Text; }
            set { tbPath.Text = value; }
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Button_Cansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


    }
}
