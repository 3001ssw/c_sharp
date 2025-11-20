using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger
{

    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "초기 메시지입니다.";

        [RelayCommand]
        private void ChangeMessage()
        {
            Message = "버튼 클릭으로 변경되었습니다!";
        }

    }
}
