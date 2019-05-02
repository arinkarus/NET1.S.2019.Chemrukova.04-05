using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleFormatting.Tests
{
    public class DoubleFormatProviderTests
    {
        [TestCase(255.255, "{0}", ExpectedResult = "255.255")]
        [TestCase(255.255, "{0:B}", ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        public string Format_NumberInvariantCulture_ReturnString(double number, string formatString)
        {
            return string.Format(new DoubleFormatProvider(CultureInfo.InvariantCulture), formatString, number); 
        }

        [TestCase(16325.63, "{0:C}", ExpectedResult = "$16,325.63")]
        public string Format_NumberUSCulture_ReturnString(double number, string formatString)
        {
            return string.Format(new DoubleFormatProvider(new CultureInfo("en-US")), formatString, number);
        }
    }
}
