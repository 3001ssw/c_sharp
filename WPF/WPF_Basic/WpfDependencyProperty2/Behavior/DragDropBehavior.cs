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
using System.Xml.Linq;

namespace WpfDependencyProperty2.Behavior
{
    public class DragDropBehavior : Behavior<FrameworkElement>
    {
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

        #region override
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;

            // 드래그 시작 관련
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += OnPreviewMouseMove;

            AssociatedObject.PreviewDragOver += OnPreviewDragOver;
            AssociatedObject.PreviewDrop += OnPreviewDrop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove -= OnPreviewMouseMove;

            AssociatedObject.PreviewDragOver -= OnPreviewDragOver;
            AssociatedObject.PreviewDrop -= OnPreviewDrop;
        }
        #endregion

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    IList dragData = dataGrid.SelectedItems;

                    DataObject data = new DataObject();
                    data.SetData(DataFormats.Serializable, dragData);

                    DragDrop.DoDragDrop(dataGrid, data, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is DataGrid dataGrid)
                {
                    IList dragData = dataGrid.SelectedItems;

                    DataObject data = new DataObject();
                    data.SetData(DataFormats.Serializable, dragData);

                    DragDrop.DoDragDrop(dataGrid, data, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            foreach (string format in formats)
            {
                ICommand? command = null;
                if (format == DataFormats.FileDrop)
                    command = FilesDropCommand;
                else if (format == DataFormats.UnicodeText)
                    command = UnicodeTextDropCommand;
                else if (format == DataFormats.Serializable)
                    command = SerializableDragCommand;
                else
                    command = null;

                if (command != null)
                {
                    object data = e.Data.GetData(format);
                    bool bCan = command.CanExecute(data);
                    if (bCan)
                        e.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                    else
                        e.Effects = DragDropEffects.None;
                    e.Handled = true;
                }
            }
        }

        private void OnPreviewDrop(object s, DragEventArgs e)
        {

            string[] formats = e.Data.GetFormats();
            foreach (string format in formats)
            {
                if (e.Data.GetDataPresent(format))
                {
                    ICommand? command = null;
                    if (format == DataFormats.FileDrop)
                        command = FilesDropCommand;
                    else if (format == DataFormats.UnicodeText)
                        command = UnicodeTextDropCommand;
                    else if (format == DataFormats.Serializable)
                        command = SerializableDragCommand;
                    else
                        command = null;

                    if (command != null)
                    {
                        object data = e.Data.GetData(format);
                        bool bCan = command.CanExecute(data);
                        if (bCan)
                            command.Execute(data);
                    }
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
