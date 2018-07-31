using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataVisualDisplay.Converters
{
    /// <summary>
    /// Converts radius to diameter
    /// </summary>
    public class DoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double radius = (double)value;
            return radius * 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double diameter = (double)value;
            return diameter / 2;
        }
    }
}
