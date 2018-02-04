using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter zum konvertieren eines Boolean-Wertes in eine Visibility
    /// </summary>
    internal class InvertedBooleanToVisibilityConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Konvertiert False in Visibility.Visible und True in Visibility.Collapsed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool b)
            {
                flag = b;
            }
            return (flag ? Visibility.Collapsed : Visibility.Visible);
        }

        /// <summary>
        /// Konvertiert Visibility.Visible in False und in Visibility.Collapsed True
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return ((Visibility)value) == Visibility.Collapsed;
            }
            return false;
        }
    }
}
