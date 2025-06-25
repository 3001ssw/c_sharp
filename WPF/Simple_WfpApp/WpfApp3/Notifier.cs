using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    // INotifyPropertyChanged 인터페이스를 구현하여 속성 변경 알림 기능을 제공하는 클래스
    public class Notifier : INotifyPropertyChanged
    {
        // 속성이 변경되었을 때 호출되는 이벤트
        public event PropertyChangedEventHandler PropertyChanged;

        // 속성이 변경되었을 때 호출되는 메서드
        protected void OnPropertyChanged(string propertyName)
        {
            // 이벤트를 발생시켜서 변경된 속성 이름을 포함한 알림을 보냄
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
