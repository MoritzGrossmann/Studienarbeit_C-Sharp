using System;
using System.Globalization;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter zum konvertieren eines Int-Wertes in einen Boolean-Wert
    /// </summary>
    internal class IntToBoolConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Konvertiert einen Int-Wert > 0 in True und einen Int-Wert <= 0 in False
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (int) value > 0;
        }

        /// <summary>
        /// Konvertierung von Boolean in Int wird nicht Unterstützt
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
