using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;

namespace WpfTask.Behaviors
{
    public class DataGridBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty IsAutoScrollProperty =
            DependencyProperty.Register(
                "IsAutoScroll",
                typeof(bool),
                typeof(DataGridBehavior),
                new PropertyMetadata(false, null));

        public bool IsAutoScroll
        {
            get => (bool)GetValue(IsAutoScrollProperty);
            set => SetValue(IsAutoScrollProperty, value);
        }

        #region constructor
        public DataGridBehavior()
        {

        }
        #endregion

        #region functions


        #endregion



        #region override functions

        protected override void OnAttached()
        {
            base.OnAttached();

            // todo .. : 
            if (AssociatedObject is DataGrid dataGrid)
            {
                if (dataGrid.Items is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += OnCollectionChanged;
                }
            }

        }

        protected override void OnDetaching()
        {
            // todo .. : 
            if (AssociatedObject is DataGrid dataGrid)
            {
                if (dataGrid.Items is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= OnCollectionChanged;
                }
            }

            base.OnDetaching();
        }

        #endregion


        #region functions

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (AssociatedObject is DataGrid dataGrid && dataGrid.Items == sender)
            {
                if (sender is INotifyCollectionChanged collection)
                {
                    if (IsAutoScroll)
                    {
                        if (dataGrid.Items.Count > 0)
                        {
                            var lastItem = dataGrid.Items[dataGrid.Items.Count - 1];
                            dataGrid.ScrollIntoView(lastItem);
                        }
                    }
                }
            }
        }

        #endregion

    }
}
