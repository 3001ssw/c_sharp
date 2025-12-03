using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPopup
{
    public class MainViewModel
    {
        public MainViewModel(IDialogService dialogService) // DI로 자동 주입됨
        {
        }
        public DelegateCommand ShowDialogCommand => new(async () =>
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var result = await metroWindow.ShowInputAsync("제목", "이름을 입력하세요");

            if (result != null)
                MessageBox.Show($"안녕, {result}!");
        });
    }
}
