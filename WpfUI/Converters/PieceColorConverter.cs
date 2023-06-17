using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation_WPF.Converters
{
    public class PieceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TeamColor color)
            {
                return color == TeamColor.White ? Brushes.White : Brushes.Black;
            }

            return Brushes.Transparent; // Default value if the value is not a TeamColor
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}