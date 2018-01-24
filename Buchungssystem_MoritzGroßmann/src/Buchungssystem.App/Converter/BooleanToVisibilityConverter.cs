using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{

    /// <summary>
    /// Converter zum konvertieren eines Boolean-Wertes in eine Visibility
    /// </summary>
    internal class BooleanToVisibilityConverter : BaseConverter, IValueConverter
    { 
        /// <summary>
        /// Konvertiert True in Visibility.Visible und False in Visibility.Collapsed
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
            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        /// Konvertiert Visibility.Visible in True und in Visibility.Collapsed False
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
                return ((Visibility)value) == Visibility.Visible;
            }
            return false;
        }
    }
}