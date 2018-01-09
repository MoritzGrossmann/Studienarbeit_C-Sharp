using System;
using System.Globalization;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    class InvertBoolConverter : BaseConverter, IValueConverter
    {
        #region Constructors

        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;
            return !bValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;
            return !bValue;
        }
        #endregion
    }
}
