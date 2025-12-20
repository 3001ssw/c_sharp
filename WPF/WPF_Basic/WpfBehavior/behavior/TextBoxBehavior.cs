using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfBehavior.behavior
{
    public class TextBoxBehavior : Behavior<TextBox>
    {
        public TextBoxBehavior()
        {
            
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            // 키보드 포커스를 받았을 때
            AssociatedObject.GotKeyboardFocus += OnGotKeyboardFocus;
            // 마우스 클릭으로 포커스를 받았을 때를 대비
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= OnGotKeyboardFocus;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll();
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
