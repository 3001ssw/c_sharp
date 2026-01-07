using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfBase64
{
    public class MainViewModel : BindableBase
    {
        private BitmapImage bitmapImage = null;
        public BitmapImage BitmapImage { get => bitmapImage; set => SetProperty(ref bitmapImage, value); }

        public MainViewModel()
        {
            try
            {
                BitmapImage = new BitmapImage(new Uri($"pack://application:,,,/WpfBase64;component/Image/sample.jpg"));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e}");
            }
        }
    }
}
