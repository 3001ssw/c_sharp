using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace WpfAnimation.Behavior
{
    public class AnimationBehavior : Behavior<FrameworkElement>
    {
        public AnimationBehavior() { }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseEnter += OnMouseEnter;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= OnMouseEnter;
            AssociatedObject.MouseLeave -= OnMouseLeave;

            base.OnDetaching();
        }
        #region mouse enter, leave

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            //StartShake();
            //StartRotate();
            StartScale();
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            //StopShake();
            //StopRotate();
            StopScale();
        }
        #endregion

        #region animation
        private void StartShake()
        {
            if (AssociatedObject == null)
                return;

            if (!(AssociatedObject.RenderTransform is TranslateTransform))
                AssociatedObject.RenderTransform = new TranslateTransform();
            
            DoubleAnimation anim = new DoubleAnimation
            {
                From = -1,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(50),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
            };

            AssociatedObject.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        private void StopShake()
        {
            if (AssociatedObject?.RenderTransform is TranslateTransform)
            {
                AssociatedObject.RenderTransform.BeginAnimation(TranslateTransform.XProperty, null);
            }
        }

        private void StartRotate()
        {
            if (AssociatedObject == null)
                return;

            if (!(AssociatedObject.RenderTransform is RotateTransform))
            {
                AssociatedObject.RenderTransform = new RotateTransform();
                AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);
            }
            
            DoubleAnimation anim = new DoubleAnimation
            {
                From = -5,
                To = 5,
                Duration = TimeSpan.FromMilliseconds(10),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            AssociatedObject.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, anim);
        }

        private void StopRotate()
        {
            if (AssociatedObject?.RenderTransform is RotateTransform)
            {
                AssociatedObject.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            }
        }

        private void StartScale()
        {
            if (AssociatedObject == null)
                return;

            if (!(AssociatedObject.RenderTransform is ScaleTransform))
            {
                AssociatedObject.RenderTransform = new ScaleTransform();
                AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5); // 컨트롤 변형이 일어날 때 기준점.
            }
            
            DoubleAnimation anim = new DoubleAnimation
            {
                From = 0.9, // 시작
                To = 1.1, // 끝
                Duration = TimeSpan.FromMilliseconds(300), // 기간
                AutoReverse = true, // 처음으로 돌아감
                RepeatBehavior = RepeatBehavior.Forever, // 계속 반복
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut }, // 효과
            };

            AssociatedObject.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, anim); // x축 으로 수행
            AssociatedObject.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim); // y축 으로 수행
        }

        private void StopScale()
        {
            if (AssociatedObject?.RenderTransform is ScaleTransform)
            {
                AssociatedObject.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                AssociatedObject.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            }
        }
        #endregion

    }
}
