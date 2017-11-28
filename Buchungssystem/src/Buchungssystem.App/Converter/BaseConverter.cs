using System;
using System.Windows.Markup;

namespace Buchungssystem.App.Converter
{
    internal abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
