using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DataVisualDisplay.Converters
{
    /// <summary>   
    /// A type converter for converting image offset into render transform  
    /// </summary>   
    public class DoubleToVerticalTranslateTransformConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dblVal = (double)value;
            TranslateTransform tt = new TranslateTransform();
            tt.Y = dblVal;
            return tt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
