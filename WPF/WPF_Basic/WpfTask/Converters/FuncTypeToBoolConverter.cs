using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTask.Converters
{
    public enum FunctionType { Func1, Func2 };

    public class FuncTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FunctionType type)
            {
                bool bEqual = type.ToString().Equals(parameter.ToString());
                return bEqual;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bCheck)
            {
                if (bCheck)
                    return parameter;
                else
                    return Binding.DoNothing;

            }
            return Binding.DoNothing;
        }
    }
}
