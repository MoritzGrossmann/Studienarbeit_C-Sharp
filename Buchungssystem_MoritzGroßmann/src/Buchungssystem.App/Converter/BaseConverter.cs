using System;
using System.Windows.Markup;

namespace Buchungssystem.App.Converter
{
    /// <summary>
    /// Basisklasse für alle Converter
    /// </summary>
    internal abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
