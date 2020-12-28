using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Manager
{

    class FileIO
    {
        private readonly string Path;

        public FileIO(string path)
        {
            Path = path;

        }
       


        public ObservableCollection<Program> IOLoad()
        {
            if (!File.Exists(Path))
            {
                File.Create(Path).Dispose();
                return new ObservableCollection<Program>();
            }
            
            if (!Directory.Exists($"{Environment.CurrentDirectory}\\Folder"))
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\Folder");
            }

            using (var reader = File.OpenText(Path))
            {
                var text = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Program>>(text);

            }


        }

        public void IOSave(object data)
        {
            using (var writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(data);
                writer.Write(output);
            }
        }
        public void IOStart(Program program)
        {
            try
            {
                    
                Process.Start($"{Environment.CurrentDirectory}\\Folder\\{program.Name + GetFileExtension(program)}");
            }
            catch (Exception)
            {

                MessageBox.Show("Файл повреждён");
            }
        }
       
        public bool IOCheck(Program program)
        {
            return File.Exists($"{Environment.CurrentDirectory}\\Folder\\{program.Name + GetFileExtension(program)}");
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
    }

}
