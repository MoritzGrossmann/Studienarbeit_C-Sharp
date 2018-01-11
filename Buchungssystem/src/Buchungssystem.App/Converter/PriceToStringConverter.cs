using System;
using System.Globalization;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter zum Konvertieren eines Decimal-Wertes in einen Währungsstring
    /// </summary>
    internal class PriceToStringConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Konvertiert ein Decimal-Wert in einen Währungsstring des jeweiligen Landes
        /// Z.B. 2,50 => 2,50€ in Euro-Region
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
                var price = (decimal) value;
                return
                    $"{decimal.Round(price, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)}{culture.NumberFormat.CurrencySymbol}";
            }
            return $"{decimal.Round(0, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)}{culture.NumberFormat.CurrencySymbol}";
        }

        /// <summary>
        /// Konvertierung von String in Decimal wird nicht Unterstützt
        /// Wirft NotSupportedException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
}
