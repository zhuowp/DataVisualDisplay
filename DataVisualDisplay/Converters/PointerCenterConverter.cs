using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DataVisualDisplay.Converters
{

    /// <summary>
    /// Calculates the pointer position
    /// </summary>
    public class PointerCenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dblVal = (double)value;
            TransformGroup tg = new TransformGroup();
            RotateTransform rt = new RotateTransform();
            TranslateTransform tt = new TranslateTransform();

            tt.X = dblVal / 2;
            tg.Children.Add(rt);
            tg.Children.Add(tt);

            return tg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
