using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    internal class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            return
                $"{decimal.Round(price, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)} {culture.NumberFormat.CurrencySymbol}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
