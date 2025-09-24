using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfConverter
{
    public class BoolToStringConverter : IValueConverter
    {
        // 단방향 bool -> string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bValue)
            {
                if (bValue)
                    return "true";
                else
                    return "false";
            }
            return Binding.DoNothing;
        }

        // 양방향 string -> bool
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue && string.IsNullOrEmpty(strValue) is not true)
            {
                if (string.Equals(strValue, "true", StringComparison.OrdinalIgnoreCase) is true)
                    return true;
                else if (string.Equals(strValue, "false", StringComparison.OrdinalIgnoreCase) is true)
                    return false;
                else
                    return Binding.DoNothing;
            }
            return Binding.DoNothing;
        }
    }
}
