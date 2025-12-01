using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfDependencyProperty
{
    public class ProgressBarBehavior : Behavior<ProgressBar>
    {
        public static readonly DependencyProperty IsVisibleSmartPropety =
            DependencyProperty.Register(
                "IsVisibleSmart",
                typeof(bool),
                typeof(ProgressBarBehavior),
                new PropertyMetadata(true, IsVisibleSmartChanged));

        private static void IsVisibleSmartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBarBehavior progressBarBehavior && progressBarBehavior.AssociatedObject != null)
            {
                ProgressBar progressBar = progressBarBehavior.AssociatedObject;
                if ((bool)e.NewValue)
                    progressBar.ValueChanged += progressBarBehavior.OnValueChanged;
                else
                    progressBar.ValueChanged -= progressBarBehavior.OnValueChanged;

            }
        }

        public bool IsVisibleSmart
        {
            get => (bool)GetValue(IsVisibleSmartPropety);
            set => SetValue(IsVisibleSmartPropety, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            // todo .. : 
            AssociatedObject.ValueChanged += OnValueChanged;
        }

        protected override void OnDetaching()
        {
            // todo .. : 
            AssociatedObject.ValueChanged -= OnValueChanged;

            base.OnDetaching();
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is ProgressBar progressBar)
                UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (AssociatedObject is ProgressBar progressBar)
            {
                if (!IsVisibleSmart)
                    return;

                double pct = AssociatedObject.Maximum == 0
                    ? 0
                    : (AssociatedObject.Value / AssociatedObject.Maximum) * 100.0;

                // 색상 변경
                if (pct < 50)
                    AssociatedObject.Foreground = Brushes.Red;
                else if (pct < 80)
                    AssociatedObject.Foreground = Brushes.Gold;
                else
                    AssociatedObject.Foreground = Brushes.LimeGreen;

                if (pct >= 90)
                {
                    var anim = new DoubleAnimation
                    {
                        From = 1.0,
                        To = 0.3,
                        Duration = TimeSpan.FromSeconds(0.5),
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    AssociatedObject.BeginAnimation(UIElement.OpacityProperty, anim);
                }
                else
                {
                    AssociatedObject.BeginAnimation(UIElement.OpacityProperty, null);
                    AssociatedObject.Opacity = 1.0;
                }
            }
        }
    }
}
