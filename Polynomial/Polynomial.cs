using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class Polynomial
    {
        private const double accuracy = 0.0001;

        public double Degree
        {
            get
            {
                return this.Coefficients.Length;
            }
        }

        public double[] Coefficients { get; private set; }

        public Polynomial(double[] coefficients)
        {
            this.Coefficients = coefficients;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            int currentPower = Coefficients.Length;
            for (int i = 0; i < this.Coefficients.Length - 1; i++)
            {
                currentPower--;
                if (Coefficients[i] == 0)
                {
                    continue;
                }
                double absoluteValue = Math.Abs(Coefficients[i]);
                if (Coefficients[i + 1] >= 0)
                {
                    stringBuilder.Append($"{absoluteValue}*x^{currentPower} + ");
                }
                if (Coefficients[i + 1] < 0)
                {
                    stringBuilder.Append($"{absoluteValue}*x^{currentPower} - ");
                }
            }
            int indexOfLastCoefficient = this.Coefficients.Length - 1;
            if (Coefficients[indexOfLastCoefficient] == 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 3, 3);
            }
            else
            {
                stringBuilder.Append($"{Coefficients[indexOfLastCoefficient]}*x^{currentPower - 1}");
            }
            return stringBuilder.ToString();
        }

        public override int GetHashCode()
        {
            return (this.Coefficients).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial polynomial))
            {
                return false;
            }
            return polynomial == this;
        }

        private static void ValidatecoefficientArray(double[] coefficients)
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

        public static bool operator == (Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            if (firstPolynomial.Degree != secondPolynomial.Degree)
            {
                return false;
            }
            return IsEqualPolynomials(firstPolynomial.Coefficients, secondPolynomial.Coefficients);
        }

        public static bool operator != (Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            return !(firstPolynomial == secondPolynomial);
        }

        private static bool IsEqualPolynomials(double[] firstPolynomialCoefficients,
            double[] secondPolynomialCoefficients)
        {
            for (int i = 0; i < firstPolynomialCoefficients.Length; i++)
            {
                if (!(Math.Abs(firstPolynomialCoefficients[i] - secondPolynomialCoefficients[i]) < accuracy)) 
                {
                    return false;
                }
            }
            return true;
        }
    }
}
