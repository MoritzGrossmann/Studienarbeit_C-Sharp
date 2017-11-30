using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.App.Converter
{
    using System;
    using System.Windows.Data;
    using System.Windows;

    namespace Buchungssystem.App.Converter
    {
        class BoolToVisibleConverter : BaseConverter, IValueConverter
        {
            #region Constructors
            /// <summary>
            /// The default constructor
            /// </summary>
            public BoolToVisibleConverter() { }
            #endregion

            #region IValueConverter Members
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                bool bValue = (bool)value;
                if (bValue)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                Visibility visibility = (Visibility)value;

                if (visibility == Visibility.Visible)
                    return true;
                else
                    return false;
            }
            #endregion
        }
    }
}
