using System;
using System.Text;

namespace Polynomial
{
    public class Polynomial
    {
        private const double accuracy = 0.0001;

        public int Degree
        {
            get
            {
                return this.Coefficients.Length;
            }
        }

        public double[] Coefficients { get; private set; }

        public Polynomial(double[] coefficients)
        {
            ValidateCoefficientArray(coefficients);
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

        public static bool operator == (Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            if (firstPolynomial.Degree != secondPolynomial.Degree)
            {
                return false;
            }
            return IsEqualPolynomials(firstPolynomial, secondPolynomial);
        }

        public static bool operator != (Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            return !(firstPolynomial == secondPolynomial);
        }

        private static bool IsEqualPolynomials(Polynomial first,
            Polynomial second)
        {
            if (first == null && second == null)
            {
                return true;
            }
            if (first == null ||  second == null)
            {
                return false;
            }
            if (ReferenceEquals(first, second))
            {
                return true;
            }
            for (int i = 0; i < second.Coefficients.Length; i++)
            {
                if (!(Math.Abs(first.Coefficients[i] - second.Coefficients[i]) < accuracy)) 
                {
                    return false;
                }
            }
            return true;
        }

        public static Polynomial operator +(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            ValidatePolynomial(firstPolynomial);
            ValidatePolynomial(secondPolynomial);
            double[] coefficients = new double[Math.Max(firstPolynomial.Degree, secondPolynomial.Degree)];
            for (int i = 0; i < firstPolynomial.Degree; i++)
            {
                coefficients[i] += firstPolynomial.Coefficients[i];
            }
            for (int i = 0; i < secondPolynomial.Degree; i++)
            {
                coefficients[i] += secondPolynomial.Coefficients[i];
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator -(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            ValidatePolynomial(firstPolynomial);
            ValidatePolynomial(secondPolynomial);
            double[] coefficients = new double[Math.Max(firstPolynomial.Degree, secondPolynomial.Degree)];
            for (int i = 0; i < secondPolynomial.Degree; i++)
            {
                coefficients[i] -= secondPolynomial.Coefficients[i];
            }
            for (int i = 0; i < firstPolynomial.Degree; i++)
            {
                coefficients[i] += firstPolynomial.Coefficients[i];
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            ValidatePolynomial(firstPolynomial);
            ValidatePolynomial(secondPolynomial);

            int resultOrder = firstPolynomial.Degree + secondPolynomial.Degree - 1;
            double[] resultForces = new double[resultOrder];

            for (int i = 0; i < firstPolynomial.Degree; i++)
            {
                for (int j = 0; j < secondPolynomial.Degree; j++)
                {
                    resultForces[i + j] += firstPolynomial.Coefficients[i] * secondPolynomial.Coefficients[j];
                }
            }

            return new Polynomial(resultForces);
        }

        private static void ValidatePolynomial(Polynomial polynomial)
        {
            if (ReferenceEquals(polynomial, null))
            {
                throw new ArgumentNullException($"{nameof(polynomial)} cannot be null!");
            }
        }

    }
}
