using System;
using System.Windows.Input;

namespace Util
{
    /// <summary>
    /// 매개변수가 없는 ICommand 구현체
    /// </summary>
    public sealed class Command : ICommand
    {
        // 실행할 동작
        private readonly Action _execute;

        // 실행 가능 여부를 판단하는 함수 (없으면 항상 true)
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="execute">실행할 동작</param>
        /// <param name="canExecute">실행 가능 여부 판단 함수 (null 허용)</param>
        public Command(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 명령 실행 가능 여부 반환
        /// </summary>
        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        /// <summary>
        /// 명령 실행
        /// </summary>
        public void Execute(object parameter) => _execute();

        /// <summary>
        /// WPF CommandManager의 RequerySuggested 이벤트에 연결하여
        /// 자동으로 CanExecute 갱신
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 수동으로 CanExecute 재평가를 요청할 때 호출
        /// </summary>
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }

    /// <summary>
    /// 매개변수를 가지는 ICommand 구현체
    /// </summary>
    public sealed class Command<T> : ICommand
    {
        // 실행할 동작
        private readonly Action<T> _execute;

        // 실행 가능 여부를 판단하는 함수 (없으면 항상 true)
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="execute">실행할 동작</param>
        /// <param name="canExecute">실행 가능 여부 판단 함수 (null 허용)</param>
        public Command(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 명령 실행 가능 여부 반환
        /// </summary>
        public bool CanExecute(object parameter)
        {
            // object → T 형변환 (실패 시 default(T))
            var value = parameter is T t ? t : default(T);
            return _canExecute?.Invoke(value) ?? true;
        }

        /// <summary>
        /// 명령 실행
        /// </summary>
        public void Execute(object parameter)
        {
            // object → T 형변환 (실패 시 default(T))
            var value = parameter is T t ? t : default(T);
            _execute(value);
        }

        /// <summary>
        /// WPF CommandManager의 RequerySuggested 이벤트에 연결하여
        /// 자동으로 CanExecute 갱신
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 수동으로 CanExecute 재평가를 요청할 때 호출
        /// </summary>
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
