using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfDependencyProperty2.Behavior
{
    public class DragDropBehavior : Behavior<FrameworkElement>
    {
        private Point _mouseLeftButtonDownPosition;
        private Point _mouseLeftButtonUpPosition;

        #region Drag
        public static readonly DependencyProperty SerializableDragCommandProperty =
                DependencyProperty.Register("SerializableDragCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null));
        public ICommand SerializableDragCommand
        {
            get => (ICommand)GetValue(SerializableDragCommandProperty);
            set => SetValue(SerializableDragCommandProperty, value);
        }
        #endregion

        #region Text drop
        public static readonly DependencyProperty UnicodeTextDropCommandProperty =
                DependencyProperty.Register("UnicodeTextDropCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null));
        public ICommand UnicodeTextDropCommand
        {
            get => (ICommand)GetValue(UnicodeTextDropCommandProperty);
            set => SetValue(UnicodeTextDropCommandProperty, value);
        }
        #endregion

        #region files drop
        public static readonly DependencyProperty FilesDropCommandProperty =
                DependencyProperty.Register("FilesDropCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null));

        public ICommand FilesDropCommand
        {
            get => (ICommand)GetValue(FilesDropCommandProperty);
            set => SetValue(FilesDropCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;

            // 드래그 시작 관련
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.PreviewMouseMove += OnMouseMove;

            AssociatedObject.PreviewDragOver += OnDragOver;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.PreviewMouseMove -= OnMouseMove;

            AssociatedObject.PreviewDragOver -= OnDragOver;
            AssociatedObject.Drop -= OnDrop;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mouseLeftButtonDownPosition = e.GetPosition(null);

            if (sender is DataGrid dataGrid)
            {
                IInputElement element = dataGrid.InputHitTest(_mouseLeftButtonDownPosition);

                if (element is DependencyObject target)
                {
                    DataGridCell? cell = FindParent<DataGridCell>(target);
                    if (cell != null)
                    {
                        DataGridRow? row = FindParent<DataGridRow>(target);
                        if (row != null)
                        {
                            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                            {
                            }
                            else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                            {
                            }
                            else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control | ModifierKeys.Shift))
                            {
                            }
                            else
                            {
                                // 드래그할 데이터 추출 (FileInfo 객체)
                                IList dragData = dataGrid.SelectedItems;

                                // 데이터 패키지 생성
                                DataObject data = new DataObject();
                                // 1. 객체 자체를 담음
                                data.SetData(dragData.GetType(), dragData);
                                // 2. 파일 경로가 있다면 문자열로도 담음 (Border 쪽에서 인식하기 쉽게)
                                data.SetData(DataFormats.Serializable, dragData);

                                // 드래그 시작
                                DragDrop.DoDragDrop(dataGrid, data, DragDropEffects.Copy);
                            }
                        }
                    }
                    else
                    {
                        if (dataGrid.SelectionMode == DataGridSelectionMode.Single)
                            dataGrid.SelectedItem = null;
                        else
                            dataGrid.SelectedItems.Clear();
                    }
                }

                Type targetType = (dataGrid.SelectionUnit == DataGridSelectionUnit.FullRow) ? typeof(DataGridRow) : typeof(DataGridCell);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_mouseLeftButtonUpPosition = e.GetPosition(null);
            //if (sender is DataGrid dataGrid)
            //{
            //    if (Point.Equals(_mouseLeftButtonDownPosition, _mouseLeftButtonUpPosition))
            //    {
            //        IInputElement element = dataGrid.InputHitTest(_mouseLeftButtonUpPosition);
            //
            //        if (element is DependencyObject target)
            //        {
            //            DataGridCell? cell = FindParent<DataGridCell>(target);
            //            if (cell != null)
            //            {
            //                DataGridRow? row = FindParent<DataGridRow>(target);
            //                if (row != null)
            //                {
            //                    if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            //                    {
            //                    }
            //                    else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            //                    {
            //                    }
            //                    else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control | ModifierKeys.Shift))
            //                    {
            //                    }
            //                    else
            //                    {
            //                        bool isSelected = cell.IsSelected;
            //                        if (isSelected)
            //                        {
            //                            if (dataGrid.SelectionMode == DataGridSelectionMode.Single)
            //                                dataGrid.SelectedItem = null;
            //                            else
            //                                dataGrid.SelectedItems.Clear();
            //
            //                            row.IsSelected = true;
            //                            row.Focus();
            //                            cell.Focus();
            //                        e.Handled = true;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    Point mousePos = e.GetPosition(null);
            //    Vector diff = _mouseLeftButtonDownPosition - mousePos;
            //
            //    // 마우스가 일정 거리(시스템 설정값) 이상 움직였을 때 드래그 시작
            //    if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            //        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            //    {
            //        if (sender is DataGrid dataGrid && dataGrid.SelectedItems != null && 0 < dataGrid.SelectedItems.Count)
            //        {
            //            // 드래그할 데이터 추출 (FileInfo 객체)
            //            IList dragData = dataGrid.SelectedItems;
            //
            //            // 데이터 패키지 생성
            //            DataObject data = new DataObject();
            //            // 1. 객체 자체를 담음
            //            data.SetData(dragData.GetType(), dragData);
            //            // 2. 파일 경로가 있다면 문자열로도 담음 (Border 쪽에서 인식하기 쉽게)
            //            data.SetData(DataFormats.Serializable, dragData);
            //
            //            // 드래그 시작
            //            DragDrop.DoDragDrop(dataGrid, data, DragDropEffects.Copy);
            //        }
            //    }
            //}
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.UnicodeText) ||
                e.Data.GetDataPresent(DataFormats.Serializable))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnDrop(object s, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                var data = e.Data.GetData(DataFormats.UnicodeText);

                if (UnicodeTextDropCommand != null && UnicodeTextDropCommand.CanExecute(data))
                {
                    UnicodeTextDropCommand.Execute(data);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = e.Data.GetData(DataFormats.FileDrop);

                if (FilesDropCommand != null && FilesDropCommand.CanExecute(data))
                {
                    FilesDropCommand.Execute(data);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                var data = e.Data.GetData(DataFormats.Serializable);

                if (SerializableDragCommand != null && SerializableDragCommand.CanExecute(data))
                {
                    SerializableDragCommand.Execute(data);
                }
            }
        }
        #endregion

        public T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
