using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Polynomial.Tests
{
    public sealed class PolynomialTests
    {
        #region ToString tests

        [TestCase(new double[] { 6, 2.05, -0.7, 0 },
            ExpectedResult = "6*x^3 + 2,05*x^2 - 0,7*x")]
        [TestCase(new double[] { 8 }, ExpectedResult = "8")]
        [TestCase(new double[] { 5, 0, 1 }, ExpectedResult = "5*x^2 + 1")]
        public string ToString_ConcreteCoefficients_ReturnString(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial.ToString();
        }

        #endregion

        #region Test creation of polynomials

        [TestCase(new double[] { -55, 0.245 })]
        [TestCase(new double[] { -0.221, 0.5454, 54545 })]
        public void InitPolynomial_ConcreteValues_CreatePolynomial(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
        }

        [Test]
        public void InitPolynomial_CoefficientsIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new Polynomial(null));

        [Test]
        public void InitPolynomial_CoefficientsIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new Polynomial(new double[] { }));

        #endregion

        #region Test addition of two polynomials

        public static IEnumerable<TestCaseData> TestCasesForAddition
        {
            get
            {
                yield return new TestCaseData(new double[] { -5, 10, 15.5, 303 }, new double[] { -5 })
                    .Returns("-10*x^3 + 10*x^2 + 15,5*x + 303");
                yield return new TestCaseData(new double[] { 0.005, 0, 0, 0 }, new double[] { 0, 0 })
                    .Returns("0,005*x^3");
                yield return new TestCaseData(new double[] { 1 }, new double[] { 0.5, 5, 3 })
                   .Returns("1,5*x^2 + 5*x + 3");
                yield return new TestCaseData(new double[] { 1, 2, 3, 4, 5 }, new double[] { 0.5, 1.2, 0 })
                   .Returns("1,5*x^4 + 3,2*x^3 + 3*x^2 + 4*x + 5");
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAddition))]
        public string AddTwoPolynomialsOperator_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial + secondPolynomial;
            return result.ToString();
        }

        [Test, TestCaseSource(nameof(TestCasesForAddition))]
        public string AddTwoPolynomialsStatic_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = Polynomial.Add(firstPolynomial, secondPolynomial);
            return result.ToString();
        }

        [Test]
        public void AddTwoPolynomialsOperator_PolynomialIsNull_ThrowArgumentNullException()
        {
            Polynomial first = null;
            var second = new Polynomial(new double[] { 2, 5.5 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first + second);
        }

        [Test]
        public void AddTwoPolynomialsStatic_PolynomialIsNull_ThrowArgumentNullException()
        {
            Polynomial first = null;
            var second = new Polynomial(new double[] { 2, 5.5 });
            Assert.Throws<ArgumentNullException>(() => Polynomial.Add(first, second));
        }

        #endregion

        #region Test add value operators and static method

        public static IEnumerable<TestCaseData> TestCasesForAdditionOfValue
        {
            get
            {
                yield return new TestCaseData(new double[] { -5, 10, 15.5, 303 }, 0)
                    .Returns("-5*x^3 + 10*x^2 + 15,5*x + 303");
                yield return new TestCaseData(new double[] { 0.005, 0, 0, 0 }, 0.001)
                    .Returns("0,006*x^3 + 0,001*x^2 + 0,001*x + 0,001");
                yield return new TestCaseData(new double[] { 1, 2, 3, 4, 5 }, 0.5)
                   .Returns("1,5*x^4 + 2,5*x^3 + 3,5*x^2 + 4,5*x + 5,5");
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAdditionOfValue))]
        public string AddValueOperatorPolynomialIsLeftOperand_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var leftOperand = new Polynomial(coefficients);
            var resultPolynomial = leftOperand + value;
            return resultPolynomial.ToString();
        }

        [Test, TestCaseSource(nameof(TestCasesForAdditionOfValue))]
        public string AddValueOperatorPolynomialIsRightOperand_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var rightOperand = new Polynomial(coefficients);
            var resultPolynomial = value + rightOperand;
            return resultPolynomial.ToString();
        }

        public string AddValueStatic_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = Polynomial.Add(polynomial, value);
            return resultPolynomial.ToString();
        }

        #endregion

        #region Test subtract value operator and static method

        [Test, TestCaseSource(nameof(TestCasesForAdditionOfValue))]
        public string SubtractOperator_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            value = -value;
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = polynomial - value;
            return resultPolynomial.ToString();
        }

        [Test, TestCaseSource(nameof(TestCasesForAdditionOfValue))]
        public string SubtractStatic_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            value = -value;
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = Polynomial.Subtract(polynomial, value);
            return resultPolynomial.ToString();
        }

        #endregion

        #region Test subtraction of polynomials and static method

        public static IEnumerable<TestCaseData> TestCasesForSubtraction
        {
            get
            {
                yield return new TestCaseData(new double[] { -5, 4, -1.5 }, new double[] { 9.54, 1, 1.5 })
                    .Returns("-14,54*x^2 + 3*x - 3");
                yield return new TestCaseData(new double[] { 0, -1 }, new double[] { -0.5, -0.01, 0, 0.255, 30 })
                    .Returns("0,5*x^4 - 0,99*x^3 - 0,255*x - 30");
                yield return new TestCaseData(new double[] { -5 }, new double[] { 3, -2.5, 0.23 })
                   .Returns("-8*x^2 + 2,5*x - 0,23");
                yield return new TestCaseData(new double[] { 15, 30, 0, 0 }, new double[] { 15, 30, 0 })
                   .Returns(string.Empty);
            }
        }

        [Test]
        public void SubtractPolynomialsOperator_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 3, 10 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first - second);
        }

        [Test, TestCaseSource(nameof(TestCasesForSubtraction))]
        public string SubtractPolynomialsOperator_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial - secondPolynomial;
            return result.ToString();
        }

        [Test]
        public void SubtractPolynomialsStatic_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 1, 2 });
            Assert.Throws<ArgumentNullException>(() => Polynomial.Subtract(first, second));
        }

        [Test, TestCaseSource(nameof(TestCasesForSubtraction))]
        public string SubtractPolynomialsStatic_ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = Polynomial.Subtract(firstPolynomial, secondPolynomial);
            return result.ToString();
        }

        #endregion

        #region Test multiply operator and static method (by value)

        public static IEnumerable<TestCaseData> TestCasesForMultiplicationByValue
        {
            get
            {
                yield return new TestCaseData(new double[] { -5, 4, -1.5 }, 2)
                    .Returns("-10*x^2 + 8*x - 3");
                yield return new TestCaseData(new double[] { -9, -6 }, -2).Returns("18*x + 12");
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForMultiplicationByValue))]
        public string MultiplyOperatorPolynomIsLeftOperand_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = polynomial * value;
            return resultPolynomial.ToString();
        }

        [Test, TestCaseSource(nameof(TestCasesForMultiplicationByValue))]
        public string MultiplyOperatorPolynomIsRightOperand_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = value * polynomial;
            return resultPolynomial.ToString();
        }

        [Test, TestCaseSource(nameof(TestCasesForMultiplicationByValue))]
        public string MultiplyStatic_ConcreteCoefficients_ReturnPolynomial(double[] coefficients, double value)
        {
            var polynomial = new Polynomial(coefficients);
            var resultPolynomial = Polynomial.Multiply(polynomial, value);
            return resultPolynomial.ToString();
        }

        #endregion

        #region Tests for multiplication

        public static IEnumerable<TestCaseData> TestCasesForMultiplication
        {
            get
            {
                yield return new TestCaseData(new double[] { 3, 2, -1 }, new double[] { -5, 0, 0, -3 })
                    .Returns("-15*x^5 - 10*x^4 + 5*x^3 - 9*x^2 - 6*x + 3");
                yield return new TestCaseData(new double[] { 2, 0, 1 }, new double[] { 1, 4 })
                    .Returns("2*x^3 + 8*x^2 + 1*x + 4");
                yield return new TestCaseData(new double[] { -3, 2 }, new double[] { 1, -7, 1 })
                   .Returns("-3*x^3 + 23*x^2 - 17*x + 2");
            }
        }

        [Test]
        public void MultiplyPolynomialsOperator_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 3, 0.54, 2121 });
            Polynomial result;
            Assert.Throws<ArgumentNullException>(() => result = first * second);
        }

        [Test, TestCaseSource(nameof(TestCasesForMultiplication))]
        public string MultiplyPolynomialsOperator__ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = firstPolynomial * secondPolynomial;
            return result.ToString();
        }

        [Test]
        public void MultiplyPolynomialsStatic_PolynomialIsNull_ArgumentNullException()
        {
            Polynomial second = null;
            var first = new Polynomial(new double[] { 10 });
            Assert.Throws<ArgumentNullException>(() => Polynomial.Multiply(first, second));
        }

        [Test, TestCaseSource(nameof(TestCasesForMultiplication))]
        public string MultiplyPolynomialsStatic__ConcreteCoefficients_ReturnNewPolynomial(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Polynomial result = Polynomial.Multiply(firstPolynomial, secondPolynomial);
            return result.ToString();
        }

        #endregion 

        #region Test indexer 

        [TestCase(-4, new double[] { 5, 4, 1 })]
        [TestCase(1, new double[] { 2 })]
        public void Indexer_IndexOutOfRange_ThrowArgumentOutOfRangeException(int index, double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            Assert.Throws<ArgumentOutOfRangeException>(() => { double x = polynomial[index]; });
        }

        [TestCase(1, new double[] { 1, 3, 10 }, ExpectedResult = 3)]
        [TestCase(4, new double[] { 0.5, 0.3, 0.3, 2, 1 }, ExpectedResult = 1)]
        public double Indexer_CorrectIndexPassed_ReturnValue(int index, double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial[index];
        }

        #endregion

        #region Tests for properties 

        [TestCase(new double[] { 1 }, ExpectedResult = 0)]
        [TestCase(new double[] { 1, 2, 3 }, ExpectedResult = 2)]
        [TestCase(new double[] { 0.002, 45 }, ExpectedResult = 1)]
        public int DegreeProperty_ConcreteCoefficients_ReturnDegree(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);
            return polynomial.Degree;
        }

        #endregion

        #region TestCases for equals methods 

        public static IEnumerable<TestCaseData> EqualPolynomialsTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { -5, 10, 15.5, 303 }, new double[] { -5 })
                  .Returns(false);
                yield return new TestCaseData(new double[] { 10, 10, 10 }, new double[] { 0, 0 })
                    .Returns(false);
                yield return new TestCaseData(new double[] { 1.5, 2, 3 }, new double[] { 1.51, 2, 3 })
                   .Returns(false);
            }
        }

        public static IEnumerable<TestCaseData> NotEqualPolynomialTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 0.5000001, 5, 10 }, new double[] { 0.5, 5, 10 })
                    .Returns(true);
                yield return new TestCaseData(new double[] { 10, 10, 10.2 }, new double[] { 10, 10, 10.2 })
                    .Returns(true);
                yield return new TestCaseData(new double[] { 3.5, 11 }, new double[] { 3.5, 11 })
                    .Returns(true);
            }
        }

        #endregion

        #region Test instance Equals method

        [Test]
        public void Equals_NotPolynomial_ReturnFalse()
        {
            var notPolynomial = new object();
            var polynomial = new Polynomial(new double[] { 1 });
            Assert.IsFalse(polynomial.Equals(notPolynomial));
        }

        [Test, TestCaseSource(nameof(NotEqualPolynomialTestCases))]
        public bool Equals_NotEqualCoefficients_ReturnFalse(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            return firstPolynomial.Equals(secondPolynomial);
        }

        [Test, TestCaseSource(nameof(EqualPolynomialsTestCases))]
        public bool Equals_EqualCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            return firstPolynomial.Equals(secondPolynomial);
        }

        [TestCase(new double[] { 0, 25 })]
        [TestCase(new double[] { 0.021, 0.0522, 0.2 })]
        public void Equals_EqualReference_ReturnTrue(double[] coefficients)
        {
            var firstPolynomial = new Polynomial(coefficients);
            var secondPolynomial = firstPolynomial;
            Assert.IsTrue(firstPolynomial.Equals(secondPolynomial));
        }

        [Test]
        public void Equals_NullPassed_ReturnFalse()
        {
            Assert.IsFalse(new Polynomial(new double[] { 0.1, 5 }).Equals(null));
        }

        #endregion

        #region Test == operator

        [Test, TestCaseSource(nameof(EqualPolynomialsTestCases))]
        public bool EqualsOperator_EqualCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            return firstPolynomial == secondPolynomial;
        }

        [Test, TestCaseSource(nameof(NotEqualPolynomialTestCases))]
        public bool EqualsOperator_NotEqualCoefficients_ReturnFalse(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return first == second;
        }

        [Test]
        public void EqualsOperator_EqualReferences_ReturnTrue()
        {
            var first = new Polynomial(new double[] { 1, 1 });
            var second = first;
            Assert.IsTrue(first == second);
        }

        [Test]
        public void EqualsOperator_NullLeftPolynomialPassed_ReturnFalse()
        {
            var polynomial = new Polynomial(new double[] { 1, 4 });
            Polynomial nullReference = null;
            Assert.IsFalse(nullReference == polynomial);
        }

        [Test]
        public void EqualsOperator_NullRightPolynomialPassed_ReturnFalse()
        {
            var polynomial = new Polynomial(new double[] { 1 });
            Polynomial nullReference = null;
            Assert.IsFalse(polynomial == nullReference);
        }

        #endregion

        #region Test static Equals method

        [Test]
        [TestCaseSource(nameof(EqualPolynomialsTestCases))]
        public bool EqualsStatic_EqualCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return Polynomial.Equals(first, second);
        }

        [Test]
        [TestCaseSource(nameof(NotEqualPolynomialTestCases))]
        public bool EqualsStatic_NotEqualCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return Polynomial.Equals(first, second);
        }

        [Test]
        public void EqualsStatic_EqualReferences_ReturnTrue()
        {
            var first = new Polynomial(new double[] { 10 });
            Polynomial second = first;
            Assert.IsTrue(Polynomial.Equals(first, second));
        }

        public void EqualsStatic_NullPassed_ReturnFalse()
        {
            var first = new Polynomial(new double[] { 10 });
            Assert.IsTrue(Polynomial.Equals(first, null));
        }

        #endregion

        #region Test != operator

        public static IEnumerable<TestCaseData> NotEqualPolynomialsForNotEqualTests
        {
            get
            {
                yield return new TestCaseData(new double[] { 0.121, 21, 21.22 }, new double[] { -5 })
                  .Returns(true);
                yield return new TestCaseData(new double[] { -0.54, 54.545 }, new double[] { 0.54, 54.545 })
                    .Returns(true);
            }
        }

        public static IEnumerable<TestCaseData> EqualPolynomialsForNotEqualTests
        {
            get
            {
                yield return new TestCaseData(new double[] { 10, 10, 10.2 }, new double[] { 10, 10, 10.2 })
                    .Returns(false);
                yield return new TestCaseData(new double[] { 66.21, 1, 23 }, new double[] { 66.21, 1, 23 })
                    .Returns(false);
            }
        }

        [Test, TestCaseSource(nameof(NotEqualPolynomialsForNotEqualTests))]
        public bool NotEqualsOperator_NotEqualsCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return first != second;
        }

        [Test, TestCaseSource(nameof(EqualPolynomialsForNotEqualTests))]
        public bool NotEqualsOperator_EqualsCoefficients_ReturnFalse(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return first != second;
        }

        #endregion

        #region Tests for static Compare method 

        [Test, TestCaseSource(nameof(NotEqualPolynomialsForNotEqualTests))]
        public bool Compare_NotEqualsCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return Polynomial.Compare(first, second);
        }

        [Test, TestCaseSource(nameof(EqualPolynomialsForNotEqualTests))]
        public bool Compare_EqualsCoefficients_ReturnFalse(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var first = new Polynomial(coefficientsForFirst);
            var second = new Polynomial(coefficientsForSecond);
            return Polynomial.Compare(first, second);
        }

        #endregion

        #region Test GetHashCode method

        [TestCase(new double[] { 1, -1.11 }, new double[] { 1.0, -5.1, 522, 21, 1020, 0.212 })]
        [TestCase(new double[] { 202 }, new double[] { 202, 0, 445, 44, 4 })]
        [TestCase(new double[] { 202 }, new double[] { 202, 101.111 })]
        public void GetHashCode_EqualCoefficients_ReturnFalse(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Assert.AreNotEqual(firstPolynomial.GetHashCode(), secondPolynomial.GetHashCode());
        }

        [TestCase(new double[] { 2.1111, 4.2121, 14.4 }, new double[] { 2.1111, 4.2121, 14.4 })]
        [TestCase(new double[] { 302.121, 20, 54 }, new double[] { 302.121, 20, 54 })]
        public void GetHashCode_EqualCoefficients_ReturnTrue(double[] coefficientsForFirst, double[] coefficientsForSecond)
        {
            var firstPolynomial = new Polynomial(coefficientsForFirst);
            var secondPolynomial = new Polynomial(coefficientsForSecond);
            Assert.AreEqual(firstPolynomial.GetHashCode(), secondPolynomial.GetHashCode());
        }

        #endregion
    }
}