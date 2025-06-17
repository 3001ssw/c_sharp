using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp3
{
    // View와 Model 사이에서 중간 역할을 하는 클래스
    // INotifyPropertyChanged: 바인딩된 속성 값이 변경되었음을 View에 알림
    public class UserViewModel : INotifyPropertyChanged
    {
        private string name;
        private int age;

        // 사용자 이름 속성
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));  // 이름이 변경되면 UI에 알림
            }
        }

        // 사용자 나이 속성
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));  // 나이가 변경되면 UI에 알림
            }
        }

        // 저장 명령을 담는 ICommand 구현체
        public RelayCommand SaveCommand { get; }

        // 생성자
        public UserViewModel()
        {
            // 명령 객체 생성: 실행 메서드 Save, 실행 가능 여부 CanSave
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        // SaveCommand가 실행할 메서드: Model 생성 + 가상 저장 처리
        private void Save()
        {
            var user = new User { Name = this.Name, Age = this.Age };
            MessageBox.Show($"사용자 저장됨: {user.Name}, {user.Age}");
        }

        // SaveCommand가 실행 가능한지 판단하는 조건
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        // 속성 변경 이벤트
        public event PropertyChangedEventHandler PropertyChanged;

        // 속성 변경 이벤트를 호출하는 메서드
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
