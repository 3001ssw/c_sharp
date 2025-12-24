using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfBehavior
{
    public class TextBoxBehavior : Behavior<TextBox>
    {
        public TextBoxBehavior()
        {
            
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            
            AssociatedObject.GotKeyboardFocus += OnGotKeyboardFocus; // 키보드 포커스를 받았을 때
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown; // 마우스 클릭으로 포커스를 받았을 때
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            // 이벤트 연결 해제
            AssociatedObject.GotKeyboardFocus -= OnGotKeyboardFocus;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll(); // 모두 선택
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 이미 포커스가 있는 상태에서 클릭한 건 무시
            if (!AssociatedObject.IsKeyboardFocusWithin)
            {
                AssociatedObject.Focus();
                e.Handled = true; // 클릭 이벤트가 텍스트 커서 위치를 바꿔버리는 걸 방지
            }
        }
    }
}
