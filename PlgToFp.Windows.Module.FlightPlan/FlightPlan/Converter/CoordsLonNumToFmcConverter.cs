using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converter
{
    /// <summary>
    /// Converts a double value of coordinates latitude from decimal number
    /// to a FMC format
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class CoordsLonNumToFmcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
                return null;

            var num = (double)value;
            var sb = new StringBuilder();

            if (num < 0)
            {
                sb.Append("W");
                num *= -1;
            }
            else
            {
                sb.Append("E");
            }

            int wholePart = (int)num;
            sb.Append(wholePart.ToString("000") + "°");

            double minsSecs = (num - wholePart) * 60;

            int wholeMins = (int)minsSecs;
            sb.Append(wholeMins.ToString("00"));

            double minTenths = (minsSecs - wholeMins) * 10;

            int wholeTenths = (int)minTenths;
            sb.Append("." + wholeTenths.ToString("0"));

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
