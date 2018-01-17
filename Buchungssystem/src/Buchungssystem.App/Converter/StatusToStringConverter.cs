using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Converter zum Konvertieren eines BookingStatus-Wertes in einen String
    /// </summary>
    internal class StatusToStringConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Converter zum Konvertieren eines BookingStatus-Wertes in einen String
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
                var val = (BookingStatus)value;
                if (val == BookingStatus.Paid)
                {
                    return "Bezahlt";
                }
                if (val == BookingStatus.Cancled)
                {
                    return "Storniert";
                }
                else
                {
                    return "Offen";
                }
            }

            return "unbekannter Status";
        }

        /// <summary>
        /// Konvertierung von String in Status wird nicht Unterstützt
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
