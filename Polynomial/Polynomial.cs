using System;
using System.Configuration;
using System.Text;
namespace Polynomial
{
    /// <summary>
    /// Class that contains methods for work with polynomials.
    /// </summary>
    public class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        #region Constants and fields
        private const double DefaultAccuracy = 0.0001;

        /// <summary>
        /// Accuracy for calculations.
        /// </summary>
        private static readonly double Accuracy;

        /// <summary>
        /// Array of coefficients. 
        /// </summary>
        private readonly double[] coefficients;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the biggest power of polynomial.
        /// </summary>
        public int Degree
        {
            get
            {
                return this.coefficients.Length - 1;
            }
        }

        /// <summary>
        /// Returns coefficient at index. 
        /// </summary>
        /// <param name="index">Index of needed coefficient.</param>
        /// <returns>Value at index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if there is no such value at index.</exception>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= this.coefficients.Length)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(index)} there is no such element.");
                }

                return this.coefficients[index];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="coefficients">Array of coefficients.</param>
        public Polynomial(params double[] coefficients)
        {
            ValidateCoefficientArray(coefficients);
            this.coefficients = SetCoefficients(coefficients);
        }

        /// <summary>
        /// Initializes static members of the <see cref="Polynomial"/> class.
        /// </summary>
        static Polynomial()
        {
            try
            {
                var reader = new AppSettingsReader();
                var settingsReader = new AppSettingsReader();
                double accuracyFromSettings = (double)settingsReader.GetValue("accuracy", typeof(double));
                Accuracy = accuracyFromSettings;
            }
            catch
            {
                Accuracy = DefaultAccuracy;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Tells if two polynomials are equal. 
        /// </summary>
        /// <param name="leftHandSidePolynomial">First operand.</param>
        /// <param name="rightHandSidePolynomial">Second operand.</param>
        /// <returns>True - if polynomials are equal, otherwise - false.</returns>
        public static bool operator ==(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return IsEqualPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Compares polynomials.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>True - if polynomials ara not equal, otherwise - false.</returns>
        public static bool operator !=(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return !(leftHandSidePolynomial == rightHandSidePolynomial);
        }

        /// <summary>
        /// Compares polynomials.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>True - if polynomials ara not equal, otherwise - false.</returns>
        public static bool Compare(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return !(leftHandSidePolynomial == rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which coefficients are increased by value.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="valueToAdd">Value to add.</param>
        /// <returns>New polynomial which coefficients are increased by value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given polynomial is null.</exception>
        public static Polynomial operator +(Polynomial polynomial, double valueToAdd)
        {
            return GetPolynomialWithNewCoefficients(polynomial, valueToAdd);
        }

        /// <summary>
        /// Returns new polynomial which coefficients are increased by value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <param name="polynomial">Given polynomial.</param>
        /// <returns>New polynomial which coefficients are increased by value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given polynomial is null.</exception>
        public static Polynomial operator +(double value, Polynomial polynomial)
        {
            return GetPolynomialWithNewCoefficients(polynomial, value);
        }

        /// <summary>
        /// Returns new polynomial which coefficients are sum of coefficients of two given polynomials.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of given polynomials is null.</exception>
        public static Polynomial operator +(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return AddTwoPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which coefficients are increased by value.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="value">Value to add.</param>
        /// <returns>New polynomial which coefficients are increased by value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when given polynomial is null.</exception>
        public static Polynomial Add(Polynomial polynomial, double value)
        {
            return GetPolynomialWithNewCoefficients(polynomial, value);
        }

        /// <summary>
        /// Returns new polynomial which coefficients are sum of coefficients of two given polynomials.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of given polynomials is null.</exception>
        public static Polynomial Add(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return AddTwoPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which is result of deduction second polynomial from first one.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of polynomials is null.</exception>
        public static Polynomial operator -(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return SubtractPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which is result of deduction second polynomial from first one.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of polynomials is null.</exception>
        public static Polynomial Subtract(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return SubtractPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which coefficients equals 
        /// first's coefficients deducted by given value.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="valueToSubtract">Value that will be deducted from polynomial's coefficients.</param>
        /// <returns>New polynomial.</returns>
        public static Polynomial operator -(Polynomial polynomial, double valueToSubtract)
        {
            return GetPolynomialWithNewCoefficients(polynomial, -valueToSubtract);
        }

        /// <summary>
        /// Returns new polynomial which coefficients equals 
        /// first's coefficients deducted by given value.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="value">Value that will be deducted from polynomial's coefficients.</param>
        /// <returns>New polynomial.</returns>
        public static Polynomial Subtract(Polynomial polynomial, double value)
        {
            return GetPolynomialWithNewCoefficients(polynomial, -value);
        }

        /// <summary>
        /// Returns new polynomial which is result of multiplication first given
        /// polynomial and second.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial/</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of polynomials is null.</exception>
        public static Polynomial operator *(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return MultiplyTwoPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Returns new polynomial which is result of multiplication first given
        /// polynomial and second.
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial/</param>
        /// <returns>New polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when one of polynomials is null.</exception>
        public static Polynomial Multiply(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return MultiplyTwoPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Multiplies coefficients of polynomial and returns new polynomial.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="value">Value that all coefficients will be multiplied by.</param>
        /// <returns>New polynomial with updated coefficients.</returns>
        public static Polynomial operator *(Polynomial polynomial, double value)
        {
            return GetPolynomialWithMultipliedCoefficients(polynomial, value);
        }

        /// <summary>
        /// Multiplies coefficients of polynomial and returns new polynomial.
        /// </summary>
        /// <param name="value">Value that all coefficients will be multiplied by.</param>
        /// <param name="polynomial">Given polynomial.</param>
        /// <returns>New polynomial with updated coefficients.</returns>
        public static Polynomial operator *(double value, Polynomial polynomial)
        {
            return GetPolynomialWithMultipliedCoefficients(polynomial, value);
        }

        /// <summary>
        /// Tells if two polynomials are equal. 
        /// </summary>
        /// <param name="leftHandSidePolynomial">First polynomial.</param>
        /// <param name="rightHandSidePolynomial">Second polynomial.</param>
        /// <returns>True - if polynomials are equal, otherwise - false.</returns>
        public static bool Equals(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            return IsEqualPolynomials(leftHandSidePolynomial, rightHandSidePolynomial);
        }

        /// <summary>
        /// Multiplies coefficients of polynomial and returns new polynomial.
        /// </summary>
        /// <param name="polynomial">Given polynomial.</param>
        /// <param name="value">Value that all coefficients will be multiplied by.</param>
        /// <returns>New polynomial with updated coefficients.</returns>
        public static Polynomial Multiply(Polynomial polynomial, double value)
        {
            return GetPolynomialWithMultipliedCoefficients(polynomial, value);
        }

        /// <summary>
        /// Compares two polynomials.
        /// </summary>
        /// <param name="other">Polynomial to compare with.</param>
        /// <returns></returns>
        public bool Equals(Polynomial other)
        {
            if (other == null)
            {
                return false;
            }

            return IsEqualPolynomials(this, other);
        }

        #endregion

        #region Interfaces implementations

        /// <summary>
        /// Returns copy of polynomial.
        /// </summary>
        /// <returns>Copy of polynomial.</returns>
        public object Clone()
        {
            double[] coeffiecientsForClonePolynomial = (double[])this.coefficients;
            return new Polynomial(coeffiecientsForClonePolynomial);
        }

        #endregion

        #region Object overrides 

        /// <summary>
        /// Return string representation of polynomial.
        /// </summary>
        /// <returns>String representation of polynomial.</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (this.Degree == 0)
            {
                stringBuilder.Append(this[0]);
                return stringBuilder.ToString();
            }

            if (this[0] != 0)
            {
                if (this.Degree == 1)
                {
                    stringBuilder.Append($"{this[0]}*x");
                }
                else
                {
                    stringBuilder.Append($"{this[0]}*x^{this.Degree}");
                }
            }

            for (int i = 1; i < this.Degree + 1; i++)
            {
                if (this[i] == 0)
                {
                    continue;
                }

                double absoluteValue = Math.Abs(this[i]);
                char sign = GetSign(this[i]);
                stringBuilder.Append($" {sign} {absoluteValue}");
                if (i != this.Degree)
                {
                    stringBuilder.Append("*x");
                }

                if (i != this.Degree && i != this.Degree - 1)
                {
                    stringBuilder.Append($"^{this.Degree - i}");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Method that returns hash code of instance.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < this.Degree + 1; i++)
                {
                    hash = (hash * 23) + this[i].GetHashCode();
                }

                return hash;
            }
        }

        /// <summary>
        /// Tells if this polynomial equal to passed.
        /// </summary>
        /// <param name="obj">Polynomial to compare with.</param>
        /// <returns>True - if polynomials are equal, otherwise - false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial polynomial))
            {
                return false;
            }

            return IsEqualPolynomials(this, polynomial);
        }

        #endregion

        #region Private methods

        private static Polynomial MultiplyTwoPolynomials(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            ValidatePolynomial(leftHandSidePolynomial);
            ValidatePolynomial(rightHandSidePolynomial);
            double[] coefficients = new double[leftHandSidePolynomial.Degree + 1
                + rightHandSidePolynomial.Degree];
            for (int i = 0; i <= leftHandSidePolynomial.Degree; i++)
            {
                for (int j = 0; j <= rightHandSidePolynomial.Degree; j++)
                {
                    coefficients[i + j] += leftHandSidePolynomial[i] *
                        rightHandSidePolynomial[j];
                }
            }

            return new Polynomial(coefficients);
        }

        private static Polynomial GetPolynomialWithNewCoefficients(Polynomial polynomial, double value)
        {
            ValidatePolynomial(polynomial);
            double[] coefficients = (double[])polynomial.coefficients.Clone();
            for (int i = 0; i <= polynomial.Degree; i++)
            {
                coefficients[i] += value;
            }

            return new Polynomial(coefficients);
        }

        private static Polynomial AddTwoPolynomials(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            ValidatePolynomial(leftHandSidePolynomial);
            ValidatePolynomial(rightHandSidePolynomial);
            double[] coefficients = new double
                [Math.Max(leftHandSidePolynomial.Degree + 1, rightHandSidePolynomial.Degree + 1)];
            for (int i = 0; i <= leftHandSidePolynomial.Degree; i++)
            {
                coefficients[i] += leftHandSidePolynomial[i];
            }

            for (int i = 0; i <= rightHandSidePolynomial.Degree; i++)
            {
                coefficients[i] += rightHandSidePolynomial[i];
            }

            return new Polynomial(coefficients);
        }

        private static char GetSign(double value)
        {
            if (value >= 0)
            {
                return '+';
            }
            else
            {
                return '-';
            }
        }

        private static void ValidatePolynomial(Polynomial polynomial)
        {
            if (ReferenceEquals(polynomial, null))
            {
                throw new ArgumentNullException($"{nameof(polynomial)} cannot be null!");
            }
        }

        private static Polynomial SubtractPolynomials(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            ValidatePolynomial(leftHandSidePolynomial);
            ValidatePolynomial(rightHandSidePolynomial);
            int length = Math.Max(leftHandSidePolynomial.Degree + 1, 
                rightHandSidePolynomial.Degree + 1);
            double[] coefficients = new double[length];
            for (int i = 0; i <= rightHandSidePolynomial.Degree; i++)
            {
                coefficients[i] -= rightHandSidePolynomial[i];
            }

            for (int i = 0; i <= leftHandSidePolynomial.Degree; i++)
            {
                coefficients[i] += leftHandSidePolynomial[i];
            }

            return new Polynomial(coefficients);
        }

        private static void ValidateCoefficientArray(double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException($"{nameof(coefficients)} cannot be null.");
            }

            if (coefficients.Length == 0)
            {
                throw new ArgumentException($"{nameof(coefficients)} cannot be empty.");
            }
        }

        private static bool IsEqualPolynomials(Polynomial leftHandSidePolynomial, Polynomial rightHandSidePolynomial)
        {
            if (ReferenceEquals(leftHandSidePolynomial, null))
            {
                return false;
            }

            if (ReferenceEquals(rightHandSidePolynomial, null))
            {
                return false;
            }

            if (ReferenceEquals(leftHandSidePolynomial, rightHandSidePolynomial))
            {
                return true;
            }

            if (leftHandSidePolynomial.Degree != rightHandSidePolynomial.Degree)
            {
                return false;
            }

            for (int i = 0; i <= rightHandSidePolynomial.Degree; i++)
            {
                if (!(Math.Abs(leftHandSidePolynomial[i]
                    - rightHandSidePolynomial[i]) < Accuracy))
                {
                    return false;
                }
            }

            return true;
        }

        private static Polynomial GetPolynomialWithMultipliedCoefficients(Polynomial polynomial, double value)
        {
            ValidatePolynomial(polynomial);
            double[] coefficients = (double[])polynomial.coefficients.Clone();
            for (int i = 0; i <= polynomial.Degree; i++)
            {
                coefficients[i] = value * coefficients[i];
            }

            return new Polynomial(coefficients);
        }

        private static double[] SetCoefficients(double[] inputCoefficients)
        {
            int indexToStart = 0;
            for (int i = 0; i < inputCoefficients.Length; i++)
            {
                if (Math.Abs(inputCoefficients[i]) < Accuracy)
                {
                    indexToStart++;
                }
                else
                {
                    break;
                }
            }

            var resultCoefficients = new double[inputCoefficients.Length - indexToStart];
            if (inputCoefficients.Length - indexToStart == 0)
            {
                throw new ArgumentException($"There aren't valuable coefficients in given array: {nameof(inputCoefficients)}");
            }

            inputCoefficients.CopyTo(resultCoefficients, indexToStart);
            return resultCoefficients;
        }

        #endregion
    }
}
