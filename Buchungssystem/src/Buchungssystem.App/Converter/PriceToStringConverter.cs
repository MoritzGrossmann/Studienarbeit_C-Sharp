using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    internal class PriceToStringConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var price = (decimal) value;
                return
                    $"{decimal.Round(price, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)}{culture.NumberFormat.CurrencySymbol}";
            }
            return $"{decimal.Round(0, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)}{culture.NumberFormat.CurrencySymbol}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string val = (string) value;

                val = val.Trim();

                return decimal.Parse(val,
                    NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
            }
            return 0;
        }
    }

    internal class BooleanToVisibilityConverter : BaseConverter, IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool b)
            {
                flag = b;
            }
            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

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
