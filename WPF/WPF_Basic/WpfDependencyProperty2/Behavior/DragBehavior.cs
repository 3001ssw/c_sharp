using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfDependencyProperty2.Behavior
{
    public class DragBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DragDataProperty =
        DependencyProperty.Register("DragData",
            typeof(object),
            typeof(DragBehavior),
            new PropertyMetadata(null));

        public object DragData
        {
            get => GetValue(DragDataProperty);
            set => SetValue(DragDataProperty, value);
        }

        protected override void OnAttached()
        {
            // 마우스를 누르고 움직일 때 드래그 시작
            AssociatedObject.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object s, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && DragData != null)
            {
                // 드래그 앤 드롭 실행
                DragDrop.DoDragDrop(AssociatedObject, DragData, DragDropEffects.Move);
            }
        }
    }
}
