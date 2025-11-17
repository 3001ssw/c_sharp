using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger01
{
    public class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _count;

        // 자동 ICommand 생성
        [RelayCommand]
        private void CountCommand()
        {
            Count++;
        }

        public MainWindowViewModel()
        {

        }
    }
}
