using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace WpfDependencyProperty2
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private ObservableCollection<FileInfo> files = new ObservableCollection<FileInfo>();
        public ObservableCollection<FileInfo> Files { get => files; set => SetProperty(ref files, value); }

        private string base64String;
        public string Base64String { get => base64String; set => SetProperty(ref base64String, value); }

        public DelegateCommand<string[]> FilesDropCommand { get; private set; }
        public DelegateCommand<object> ImageFileDropCommand { get; private set; }
        #endregion

        public MainViewModel()
        {
            FilesDropCommand = new DelegateCommand<string[]>(OnFilesDropCommand, CanFilesDropCommand);
            ImageFileDropCommand = new DelegateCommand<object>(OnImageFileDropCommandDrop, CanImageFileDropCommandDrop);
        }

        private void OnFilesDropCommand(string[] files)
        {
            if (files.Count() <= 0)
                return;

            foreach (string file in files)
            {
                if (File.Exists(file) == false)
                    continue;

                FileInfo fi = new FileInfo();
                fi.FileName = Path.GetFileName(file);
                fi.FilePath = file;

                Files.Add(fi);
            }
        }

        private bool CanFilesDropCommand(string[] files)
        {
            return true;
        }

        private void OnImageFileDropCommandDrop(object obj)
        {
            if (obj is IList list && 0 < list.Count)
            {
                var files = list.Cast<FileInfo>().ToList();
                FileInfo fi = files[0];
                string file = fi.FilePath;
                try
                {
                    byte[] arrImage = File.ReadAllBytes(file);
                    Base64String = Convert.ToBase64String(arrImage);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"error: {e}");
                }
            }
        }

        private bool CanImageFileDropCommandDrop(object obj)
        {
            if (obj is IList list)
            {
                if (list.Count == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
