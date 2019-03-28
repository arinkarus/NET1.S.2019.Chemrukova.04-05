using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Polynomial.Tests
{
    public class PolynomialTests
    {
        [TestCase(new double[] {6, 2.05, -0.7, 0},
            ExpectedResult = "6*x^3 + 2,05*x^2 - 0,7*x^1")]
        [TestCase(new double[] { 8 }, ExpectedResult = "8*x^0")]
        [TestCase(new double[] { 5, 0, 1 }, ExpectedResult = "5*x^2 + 1*x^0")]
        public string ToString_ConcreteCoefficients_ReturnString(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial.ToString();
        }
    }
}
