using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace WpfBase64.Converter
{
    public class Base64StringToBitmapImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapImage bi)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));

                using (var ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();

                    string base64string = System.Convert.ToBase64String(imageBytes);
                    return base64string;
                }
            }

            return Binding.DoNothing;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s)
            {
                if (string.IsNullOrEmpty(s))
                    return null;

                try
                {
                    BitmapImage bi = new BitmapImage();

                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
                    bi.EndInit();

                    return bi;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
