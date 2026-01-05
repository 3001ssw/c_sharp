using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfDependencyProperty2.Converter
{
    public class StringToBase64Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s)
            {
                if (string.IsNullOrEmpty(s))
                    return null;

                try
                {
                    BitmapImage bi = new BitmapImage();

                    bi.BeginInit();
                    bi.StreamSource = new System.IO.MemoryStream(System.Convert.FromBase64String(s));
                    bi.EndInit();

                    return bi;
                }
                catch
                {
                    MessageBox.Show("Is not Base64 String. Checking please. : " + s);
                    return null;    // string.Empty로 할 경우, 경로 오류 문제가 생겨 null처리로 변경
                }
            }
            else
            {
                return null;    // string.Empty로 할 경우, 경로 오류 문제가 생겨 null처리로 변경
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
