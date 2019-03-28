using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Polynomial.Tests
{
    public sealed class PolynomialTests
    {
        [TestCase(new double[] { 6, 2.05, -0.7, 0 },
            ExpectedResult = "6*x^3 + 2,05*x^2 - 0,7*x^1")]
        [TestCase(new double[] { 8 }, ExpectedResult = "8*x^0")]
        [TestCase(new double[] { 5, 0, 1 }, ExpectedResult = "5*x^2 + 1*x^0")]
        public string ToString_ConcreteCoefficients_ReturnString(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial.ToString();
        }

        [Test]
        public void Polynomial_CoefficientsIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new Polynomial(null));

        [Test]
        public void Polynomial_CoefficientsIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new Polynomial(new double[] { }));

        [TestCase(new double[] { -5, 10, 15.5, 303 }, new double[] { -5 },
            ExpectedResult = new double[] { -10, 10, 15.5, 303 })]
        [TestCase(new double[] { 0.0005, 0, 0, 0 }, new double[] { 0, 0 }, ExpectedResult = new double[] { 0.0005, 0, 0, 0 })]
        [TestCase(new double[] { 1 }, new double[] { 0.5, 5, 3 }, ExpectedResult = new double[] { 1.5, 5, 3 })]
        [TestCase(new double[] { 1, 2, 3, 4, 5 }, new double[] { 0.5, 1.2, 0 }, ExpectedResult = new double[] { 1.5, 3.2, 3, 4, 5 })]
        public double[] AddTwoPolynomials_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial + secondPolynomial;
            return result.Coefficients;
        }

        [Test]
        public void AddTwoPolynomials_PolynomialIsNull_ThrowArgumentNullException()
        {
            Polynomial first = null;
            var second = new Polynomial(new double[] { 2, 5.5 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first + second);
        }

        [Test]
        public void SubtractPolynomials_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 3, 10 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first - second);
        }

        [TestCase(new double[] { -5, 4, -1.5 }, new double[] { 9.54, 1, 1.5 },
            ExpectedResult = new double[] { -14.54, 3, -3 })]
        [TestCase(new double[] { 0, -1 }, new double[] { -0.5, -0.01, 0, 0.255, 30 }, ExpectedResult = new double[] { 0.5, -0.99, 0, -0.255, -30 })]
        [TestCase(new double[] { -5 }, new double[] { 3, -2.5, 0.23 }, ExpectedResult = new double[] { -8, 2.5, -0.230 })]
        [TestCase(new double[] { 15, 30, 0, 0 }, new double[] { 15, 30, 0 }, ExpectedResult = new double[] { 0, 0, 0, 0 })]
        public double[] SubtractPolynomials_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial - secondPolynomial;
            return result.Coefficients;
        }

        [Test]
        public void MultiplyPolynomials_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 3, 0.54, 2121 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first * second);
        }

        [TestCase(new double[] { 3,  2, - 1 }, new double[] { -5, 0,  0, -3 }, 
            ExpectedResult = new double[] { -15, -10, 5, -9, -6, 3 })]
        public double[] MultiplyPolynomials__ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial * secondPolynomial;
            return result.Coefficients;
        }
    }
}
 