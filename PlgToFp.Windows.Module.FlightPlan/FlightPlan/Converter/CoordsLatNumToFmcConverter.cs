using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converter
{
    /// <summary>
    /// Converts a double value of coordinates latitude from decimal number
    /// to a FMC format
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class CoordsLatNumToFmcConverter : IValueConverter
    {
        private const string CoordRegex = @"([NS]{1})(\d{2})°?(\d{2})\.(\d)";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
                return null;

            var num = (double)value;
            var sb = new StringBuilder();

            if (num < 0)
            {
                sb.Append("S");
                num *= -1;
            }
            else
            {
                sb.Append("N");
            }

            int wholePart = (int)num;
            sb.Append(wholePart.ToString("00") + "°");

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
            if (value == null
                || (! (value is string))
                || (string.IsNullOrWhiteSpace((string)value)))
                return 0d;

            //([NS]{1})(\d{2})°?(\d{2})\.(\d)

            var m = Regex.Match((string)value, CoordRegex);

            var hemisphere = m.Groups[1].Value;
            var deg = m.Groups[2].Value;
            var min = m.Groups[3].Value;
            var dec = m.Groups[4].Value;

#if DEBUG
            Debug.Print(string.Format("parsed {0}{1}°{2}.{3}", hemisphere, deg, min, dec));
#endif
            int hemi = 0;
            if (hemisphere == "N")
                hemi = 1;
            if (hemisphere == "S")
                hemi = -1;

            int degi = 0;
            if (!int.TryParse(deg, out degi))
                return -1;

            int mini = 0;
            if (!int.TryParse(min, out mini))
                return -1;

            int deci = 0;
            if (!int.TryParse(dec, out deci))
                return -1;

            double numval = hemi * (degi + ((double)mini / 60) + ((double)deci / 600));
            return numval;
        }

        public bool ValidateString(string val)
        {
            var m = Regex.Match(val, CoordRegex);
            return m.Success;
        }
    }
}
