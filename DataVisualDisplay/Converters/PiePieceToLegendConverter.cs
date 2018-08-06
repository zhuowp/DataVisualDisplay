using DataVisualDisplay.Controls;
using DataVisualDisplay.Models.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataVisualDisplay.Converters
{
    public class PiePieceToLegendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PiePiece data = value as PiePiece;
            if(data == null)
            {
                return null;
            }

            return string.Format("{0} - {1} - {2}%", data.Name, data.Value, data.Percentage * 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
