using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMenu.Util
{
    // ICommand 인터페이스 구현
    // MVVM에서 버튼 클릭 등 명령을 처리할 때 사용
    public class Command : ICommand
    {
        private readonly Action? execute;         // 명령 실행 메서드
        private readonly Func<bool>? canExecute;  // 명령 실행 가능 여부 판단

        // 생성자: 실행 메서드와 실행 가능 조건을 받음
        public Command(Action execute, Func<bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // 버튼 등 UI 요소가 명령 실행 가능한지 판단
        public bool CanExecute(object parameter) => canExecute?.Invoke() ?? true;

        // 명령 실행
        public void Execute(object parameter) => execute?.Invoke();

        // CanExecute가 바뀔 때 UI에 알림
        public event EventHandler? CanExecuteChanged;

        // 외부에서 실행 가능 여부를 재평가하라고 알릴 때 사용
        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
