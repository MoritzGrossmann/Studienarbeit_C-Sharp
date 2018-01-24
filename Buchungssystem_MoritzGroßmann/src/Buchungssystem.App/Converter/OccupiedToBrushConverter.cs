using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter, zum Koinvertieren eines Boolean-Wertes in eine Farbe
    /// </summary>
    internal class OccupiedToBrushConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Konvertiert True in Schwarz und False in Rot
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var val = (bool) value;
                return (Brush)new BrushConverter().ConvertFrom(val ? "#f44242" : "#000000");
            }
            return (Brush)new BrushConverter().ConvertFrom("#000000");

        }

        /// <summary>
        /// Konvertierung von Brush in Bollean wird nicht Unterstützt
        /// Wirft NotSupportedException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
