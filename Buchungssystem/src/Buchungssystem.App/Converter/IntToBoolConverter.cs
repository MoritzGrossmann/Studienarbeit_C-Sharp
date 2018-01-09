﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Buchungssystem.App.Converter
{
    internal class IntToBoolConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (int) value > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
