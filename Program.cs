using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace Manager
{
	class Program : INotifyPropertyChanged
	{
		private string _program;
		private string _name;
		private string _size;
		private bool _check;
		private SolidColorBrush _icon;
		public SolidColorBrush Iconca
		{
			get { return _icon; }
			set
			{
				_icon = value;
				OnPropertyChanged("Iconca");
			}
		}

		public string Path
		{
			get { return _program; }
			set
			{
				_program = value;

				OnPropertyChanged("Programa");

			}
		}


		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged("Name");
			}
		}

		public string Size
		{
			get { return _size; }
			set { _size = value; }
		}


		public bool Check
		{
			get { return _check; }
			set 
			{ 
				_check = value;
				OnPropertyChanged("Сheck");
			}
			
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string property = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
