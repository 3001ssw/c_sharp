using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfAction
{
    public class DirectEnumToBoolConverter : IValueConverter
    {
        public enum DirectType
        {
            Top,
            Left,
            Right,
            Bottom,
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DirectType dir)
            {
                bool res = dir.ToString() == parameter?.ToString();
                return res;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return Enum.Parse(targetType, parameter?.ToString());
            }
            return Binding.DoNothing;
        }
    }
}
