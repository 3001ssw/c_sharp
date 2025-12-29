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
                "IsVisibleSmart", // 속성 이름
                typeof(bool), // 속성 데이터 타입
                typeof(ProgressBarBehavior), // 이 속성을 소유한 클래스
                new PropertyMetadata(false, IsVisibleSmartChanged)); // 기본값 (false으로 설정)

        public bool IsVisibleSmart
        {
            get => (bool)GetValue(IsVisibleSmartPropety);
            set => SetValue(IsVisibleSmartPropety, value);
        }

        private static void IsVisibleSmartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProgressBarBehavior progressBarBehavior)
            {
                progressBarBehavior.UpdateVisual();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.ValueChanged += OnValueChanged;
                UpdateVisual();
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
                AssociatedObject.ValueChanged -= OnValueChanged;

            base.OnDetaching();
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is ProgressBar progressBar)
                UpdateVisual();
        }

        private bool _isBlinking = false;
        private void UpdateVisual()
        {
            if (AssociatedObject is ProgressBar progressBar)
            {
                if (!IsVisibleSmart)
                {
                    // 원상 복귀
                    AssociatedObject.BeginAnimation(UIElement.OpacityProperty, null);
                    AssociatedObject.Opacity = 1.0;
                    AssociatedObject.Foreground = Brushes.LimeGreen;
                    _isBlinking = false;
                }
                else
                {
                    double pct = AssociatedObject.Maximum == 0 ? 0 : (AssociatedObject.Value / AssociatedObject.Maximum) * 100.0;

                    // 색상 변경
                    if (pct < 50)
                        AssociatedObject.Foreground = Brushes.Red; // 50 이하 빨간색
                    else if (pct < 80)
                        AssociatedObject.Foreground = Brushes.Gold; // 80 이하 황금색
                    else
                        AssociatedObject.Foreground = Brushes.LimeGreen; // 그 외 초록색

                    // 90 이상이면 깜빡이는 애니메이션
                    if (90 <= pct)
                    {
                        if (_isBlinking == false)
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
                            _isBlinking = true;
                        }
                    }
                    else
                    {
                        _isBlinking = false;
                        AssociatedObject.BeginAnimation(UIElement.OpacityProperty, null);
                        AssociatedObject.Opacity = 1.0;
                    }
                }
            }
        }
    }
}
