using NUnit.Framework;

namespace DoubleExtensions.Tests
{
    public class TransformTests
    {
        [TestCase(double.PositiveInfinity, ExpectedResult = "Infinity")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "-Infinity")]
        [TestCase(double.NaN, ExpectedResult = "NaN")]
        [TestCase(0, ExpectedResult = "zero")]
        [TestCase(2E+18, ExpectedResult = "two exponenta plus one eight")]
        [TestCase(double.MaxValue, ExpectedResult = "one point seven nine seven six nine three one three four eight six two three two exponenta plus three zero eight")]
        [TestCase(0.375, ExpectedResult = "zero point three seven five")]
        [TestCase(2e-15, ExpectedResult = "two exponenta minus one five")]
        [TestCase(-23.809, ExpectedResult = "minus two three point eight zero nine")]
        public string TransformToWords_DoubleValuePassed_ReturnStringRepresentation(double number)
        {
            return Transform.TransformToWords(number);
        }
    }
}
