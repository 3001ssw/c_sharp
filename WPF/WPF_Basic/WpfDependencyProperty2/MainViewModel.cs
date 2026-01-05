using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDependencyProperty2
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        public DelegateCommand<string[]> DropCommand { get; private set; }

        private string text;
        public string Text { get => text; set => SetProperty(ref text, value); }
        #endregion

        public MainViewModel()
        {
            DropCommand = new DelegateCommand<string[]>(OnDropCommand, CanDropCommand);
        }

        private void OnDropCommand(string[] files)
        {
            if (files.Count() <= 0)
                return;

            if (File.Exists(files[0]))
            {
                byte[] arrImage = File.ReadAllBytes(files[0]);
                Text = Convert.ToBase64String(arrImage);
            }
        }

        private bool CanDropCommand(string[] files)
        {
            return true;
        }
    }
}
