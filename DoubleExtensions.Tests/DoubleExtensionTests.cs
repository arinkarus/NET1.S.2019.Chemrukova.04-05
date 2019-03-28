using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleExtensions.Tests
{
    public class DoubleExtensionTests
    {
        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")] 
        public string DoubleExtensionTests_DoubleValuePassed_ReturnBinaryRepresentation(double number) =>
            number.GetIEEE754BinaryRepresentation();

        [TestCase(new double[] { 2e-15 }, ExpectedResult = new string[] { "two exponenta minus one five" })]
        [TestCase(new double[] { 222.21, 21, 0.12 },
            ExpectedResult = new string[] { "two two two point two one", "two one", "zero point one two"} )]
        [TestCase(new double[] { double.NaN, 0, double.NegativeInfinity, double.PositiveInfinity }, ExpectedResult = new string[] { "NaN", "zero", "-Infinity", "Infinity" })]
        [TestCase(new double[] { 0.234, 1.54, 0.001 }, 
            ExpectedResult = new string[] { "zero point two three four", "one point five four", "zero point zero zero one"})]
        public string[] TransformToWords_ArrayOfDoublesPassed_ReturnArrayOfStrings(double[] array) =>
            array.TransformToWords();

        public void TransformToWords_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => DoubleExtension.TransformToWords(null));

        public void TransformToWords_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new double[] { }.TransformToWords());

        [TestCase(10000, 0.547, "zero point five four seven")]
        [TestCase(1000000, 221.59, "two two one point five nine")]
        public void TransformToWords_BigArray_ReturnResult(int amountOfElements, double valueToPopulate, string expectedString)
        {
            double[] source = Enumerable.Repeat(valueToPopulate, amountOfElements).ToArray();
            string[] expected = Enumerable.Repeat(expectedString, amountOfElements).ToArray();
            string[] actual = source.TransformToWords();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
