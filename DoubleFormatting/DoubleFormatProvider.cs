using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubleExtensions;

namespace DoubleFormatting
{
    public class DoubleFormatProvider : IFormatProvider, ICustomFormatter
    {
        private readonly IFormatProvider parent;

        public DoubleFormatProvider(IFormatProvider parent)
        {
            this.parent = parent;
        }

        public DoubleFormatProvider() : this(CultureInfo.CurrentCulture)
        {

        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(double))
            {
                return HandleOtherFormats(format, arg, parent);
            }

            var number = (double)arg;
            if (format == "B")
            {
                return DoubleExtension.GetIEEE754BinaryRepresentation(number);
            }

            return number.ToString(format, parent);
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        private string HandleOtherFormats(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, formatProvider);
            }

            if (arg != null)
            {
                return arg.ToString();
            }

            return string.Empty;
        }
    }
}

