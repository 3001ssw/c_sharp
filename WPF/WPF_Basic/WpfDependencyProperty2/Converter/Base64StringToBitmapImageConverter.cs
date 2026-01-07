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

namespace WpfDependencyProperty2.Converter
{
    public class Base64StringToBitmapImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
                    Debug.WriteLine("Is not Base64 String. Checking please. : " + s);
                    return null;    // string.Empty로 할 경우, 경로 오류 문제가 생겨 null처리로 변경
                }
            }
            else
            {
                return null;    // string.Empty로 할 경우, 경로 오류 문제가 생겨 null처리로 변경
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is BitmapImage bi)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));

                using (var ms = new MemoryStream())
                {
                    // 2. 스트림에 이미지 저장
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();

                    // 3. 바이트 배열을 Base64 문자열로 변환
                    return System.Convert.ToBase64String(imageBytes);
                }
            }

            return null;
        }
    }
}
