using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PlgToFp.Windows.Infrastructure.Converters
{
    /// <summary>
    /// Converts the bool value to Visibility.
    /// If value is true or null, returns Visibility.Visible.
    /// If value is false, returns Visibility.Collapsed
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return Visibility.Visible;

            if ((bool)value)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                return false;

            var vis = (Visibility)value;
            if (vis == Visibility.Collapsed || vis == Visibility.Hidden)
                return false;

            return true;
        }
    }
}
