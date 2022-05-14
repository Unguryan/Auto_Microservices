using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UI.Converters
{
    public class StringToPositiveIntValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            int num;
            string strvalue = value as string;
            if (int.TryParse(strvalue, out num) && num > 0)
            {
                return num;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
