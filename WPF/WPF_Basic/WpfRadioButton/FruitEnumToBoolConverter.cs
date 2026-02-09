using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfRadioButton
{
    public class FruitEnumToBoolConverter : IValueConverter
    {
        public enum FruitType { Apple, Orange, Grape };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FruitType type)
            {
                if (type.ToString() == parameter.ToString())
                    return true;
                else
                    return false;
            }
            return Binding.DoNothing;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool check)
            {
                if (check)
                    return parameter;
                else
                    return Binding.DoNothing;
            }
            return Binding.DoNothing;
        }
    }
}
