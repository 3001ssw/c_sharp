using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfDependencyProperty2.Behavior
{
    public class DragBehavior : Behavior<FrameworkElement>
    {
        #region drop
        public static readonly DependencyProperty DropCommandProperty =
                DependencyProperty.Register("DropCommand",
                    typeof(ICommand),
                    typeof(DragBehavior),
                    new PropertyMetadata(null));

        public ICommand DropCommand
        {
            get => (ICommand)GetValue(DropCommandProperty);
            set => SetValue(DropCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true; // 중요: AllowDrop 활성화
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= OnDrop;
        }
        private void OnDrop(object s, DragEventArgs e)
        {
            // 1. 파일 드롭 형식인지 확인
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // 2. 파일 경로들을 string[] 형태로 가져옴
                var files = e.Data.GetData(DataFormats.FileDrop);

                // 3. 커맨드가 존재하고 실행 가능한지 확인 후 파일 데이터 전달
                if (DropCommand != null && DropCommand.CanExecute(files))
                {
                    DropCommand.Execute(files);
                }
            }
        }
        #endregion

    }
}
