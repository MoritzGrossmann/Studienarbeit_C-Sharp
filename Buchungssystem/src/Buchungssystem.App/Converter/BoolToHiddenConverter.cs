using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    class BoolToHiddenConverter : BaseConverter, IValueConverter
    {
            #region Constructors

            #endregion

            #region IValueConverter Members
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool bValue = value != null && (bool)value;
                if (!bValue)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                Visibility? visibility = (Visibility?)value;

                if (visibility == Visibility.Visible)
                    return false;
                return false;
            }
            #endregion
    }
}
