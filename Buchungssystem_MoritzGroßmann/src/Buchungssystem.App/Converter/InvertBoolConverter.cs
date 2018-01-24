using System;
using System.Globalization;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter zum Invertieren eines Boolean-Wertes
    /// </summary>
    class InvertBoolConverter : BaseConverter, IValueConverter
    {
        #region Constructors

        #endregion

        /// <summary>
        /// Konvertiert True in False und False in True
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;
            return !bValue;
        }

        /// <summary>
        /// Konvertiert True in False und False in True
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;
            return !bValue;
        }
    }
}
