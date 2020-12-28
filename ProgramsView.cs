using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Media;

namespace Manager
{
    class ProgramsView : INotifyPropertyChanged
    {
        private bool _ButtonCoiser;
        public ObservableCollection<Program> Programs { get; set; }

        private Program _selectedItem;
        private FileIO _fileIO;

        private Commands _addcommands;
        private Commands _removecommands;
        private Commands _saveCommand;
        private Commands _downloadsCommand;
        private Commands _downloadCommand;
        private Commands _copyCommand;
        private Commands _checkCommand;
        private Commands _startCommand;
        private Commands _allchoiceCommand;
        private Commands _changeCommand;

        public Commands ChangeCommand
        {
            get
            {
                return _changeCommand ??
                  (_changeCommand = new Commands(obj =>
                  {

                      var change = new Change();
                      change.NameProgram = SelectedItem.Name;
                      change.Path = SelectedItem.Path;
                      if (change.ShowDialog() == true)
                      {
                          var size = Checker(change.Path);
                          if (size > 0)
                          {
                              Program prog = obj as Program;
                              prog = new Program { Name = change.NameProgram, Path = change.Path, Size = size.ToString("# KB") };
                              if (Programs == null)
                              {
                                  Programs = new ObservableCollection<Program> { SelectedItem as Program };
                              }
                              else
                              {
                                  Programs.Remove(SelectedItem);
                                  Programs.Insert(0, prog);
                              }



                              Checker();
                              Save(false);
                          }
                          else
                          {
                              MessageBox.Show("Неудача!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                          }

                      }
                  }, (obj) => SelectedItem != null));
            }
        }
        public Commands AllChoiceCommand
        {
            get
            {
                return _allchoiceCommand ??
                  (_allchoiceCommand = new Commands(obj =>
                  {
                      if (_ButtonCoiser == true)
                      {
                          _ButtonCoiser = false;
                      }
                      else
                      {
                          _ButtonCoiser = true;
                      }
                      foreach (var item in Programs)
                      {

                          item.Check = _ButtonCoiser;

                      };


                  }));
            }
        }
        public Commands StartCommand
        {
            get
            {
                return _startCommand ??
                  (_startCommand = new Commands(obj =>
                  {
                      Checker(SelectedItem);
                      if (_fileIO.IOCheck(SelectedItem))
                      {
                          _fileIO.IOStart(SelectedItem);
                      }
                      else
                      {
                          MessageBox.Show("Установите программу");
                      }


                  }, (obj) => SelectedItem != null));
            }
        }
        public Commands CheckCommand
        {
            get
            {
                return _checkCommand ??
                  (_checkCommand = new Commands(obj =>
                  {
                      Checker();
                      Save(false);
                  }));
            }
        }
        public Commands CopyCommand
        {
            get
            {
                return _copyCommand ??
                  (_copyCommand = new Commands(obj =>
                  {
                      Clipboard.SetData(DataFormats.Text, SelectedItem.Path);
                      MessageBox.Show("Копирование", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);

                  }, (obj) => SelectedItem != null));
            }
        }
        public Commands DownloadsCommand
        {
            get
            {
                _fileIO.IOSave(Programs);
                return _downloadsCommand ??
                  (_downloadsCommand = new Commands(obj =>
                  {

                      
                      var winLoad = new Download(Programs, false);
                      if (winLoad.ShowDialog() == true)
                      {
                          Checker();
                      }
                      Save(false);



                  }));
            }
        }
        public Commands DownloadCommand
        {
            get
            {
                _fileIO.IOSave(Programs);
                return _downloadCommand ??
                  (_downloadsCommand = new Commands(obj =>
                  {


                      var winLoad = new Download(SelectedItem, true);
                      if (winLoad.ShowDialog() == true)
                      {
                          Checker();
                      }
                      Save(false);



                  },(obj) => SelectedItem != null));
            }
        }
        public Commands SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new Commands(obj =>
                  {
                      Save(true);
                  }));
            }
        }
        public Commands RemoveCommand
        {
            get
            {
                return _removecommands ??
                    (_removecommands = new Commands(obj =>
                    {
                        if (MessageBox.Show("Правда хотите удалить?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Program prog = obj as Program;
                            if (prog != null)
                            {
                                Programs.Remove(prog);

                            }
                        }

                    },
                    (obj) => SelectedItem != null));
            }
        }
        public Commands AddCommand
        {
            get
            {
                return _addcommands ??
                  (_addcommands = new Commands(obj =>
                  {
                      Load load = new Load();
                      if (load.ShowDialog() == true)
                      {
                          var size = Checker(load.Path);
                          if (size > 0)
                          {

                              Program prog = new Program { Name = load.NameProgram, Path = load.Path, Size = size.ToString("# KB") };
                              if (Programs == null)
                              {
                                  Programs = new ObservableCollection<Program> { prog as Program };
                              }
                              else
                              {
                                  Programs.Insert(0, prog);
                              }

                              SelectedItem = prog;

                              Checker();
                              Save(false);
                          }
                          else
                          {
                              MessageBox.Show("Ошибка!", "Размер файла равен нулю.", MessageBoxButton.OK, MessageBoxImage.Error);
                          }

                      }
                  }));
            }

        }
        public Program SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ProgramsView()
        {
            _fileIO = new FileIO($"{Environment.CurrentDirectory}\\Download.json");
            Programs = _fileIO.IOLoad();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private void Save(bool check)
        {
            if (check == true)
            {
                try
                {
                    _fileIO.IOSave(Programs);
                    MessageBox.Show("Сохранено", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не сохранено", "Неудачно", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    _fileIO.IOSave(Programs);
                }
                catch (Exception)
                {
                    MessageBox.Show("Не сохранено", "Ошибка Сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
        private void Checker()
        {

            foreach (var item in Programs)
            {
                if (_fileIO.IOCheck(item))
                {
                    item.Iconca = new SolidColorBrush(Colors.Turquoise);

                }
                else
                {

                    item.Iconca = new SolidColorBrush(Colors.White);

                }
            }
        }
        private void Checker(Program item)
        {

            if (_fileIO.IOCheck(item))
            {
                item.Iconca = new SolidColorBrush(Colors.Turquoise);

            }
            else
            {

                item.Iconca = new SolidColorBrush(Colors.White);

            }

        }
        private long Checker(string path)
        {
            try
            {
                HttpWebRequest web = WebRequest.CreateHttp(path);
                using (WebResponse response = web.GetResponse())
                {
                    return (Convert.ToInt64(response.ContentLength) / 1024);
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Ссылка не является действительной!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

        }
    }
}
