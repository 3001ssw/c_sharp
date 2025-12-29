using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfDependencyProperty
{
    public class SmartProgressBar : ProgressBar
    {
        public static readonly DependencyProperty IsVisibleSmartPropety =
            DependencyProperty.Register(
                "IsVisibleSmart", // 속성 이름
                typeof(bool), // 속성 데이터 타입
                typeof(SmartProgressBar), // 이 속성을 소유한 클래스
                new PropertyMetadata(false, IsVisibleSmartChanged)); // 기본값 (false으로 설정)

        public bool IsVisibleSmart
        {
            get => (bool)GetValue(IsVisibleSmartPropety);
            set => SetValue(IsVisibleSmartPropety, value);
        }

        private static void IsVisibleSmartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SmartProgressBar smartProgressBar)
            {
                smartProgressBar.UpdateVisual();
            }
        }

        public SmartProgressBar()
        {
            this.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is SmartProgressBar progressBar)
                UpdateVisual();
        }

        private bool _isBlinking = false;
        private void UpdateVisual()
        {
            if (!IsVisibleSmart)
            {
                // 원상 복귀
                BeginAnimation(UIElement.OpacityProperty, null);
                Opacity = 1.0;
                Foreground = Brushes.LimeGreen;
                _isBlinking = false;
            }
            else
            {
                double pct = Maximum == 0 ? 0 : (Value / Maximum) * 100.0;

                // 색상 변경
                if (pct < 50)
                    Foreground = Brushes.Red; // 50 이하 빨간색
                else if (pct < 80)
                    Foreground = Brushes.Gold; // 80 이하 황금색
                else
                    Foreground = Brushes.LimeGreen; // 그 외 초록색

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
                        BeginAnimation(UIElement.OpacityProperty, anim);
                        _isBlinking = true;
                    }
                }
                else
                {
                    _isBlinking = false;
                    BeginAnimation(UIElement.OpacityProperty, null);
                    Opacity = 1.0;
                }
            }
        }
    }
}
