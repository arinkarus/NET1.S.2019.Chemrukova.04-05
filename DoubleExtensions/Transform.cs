using System;
using System.Collections.Generic;
using System.Text;

namespace DoubleExtensions
{
    /// <summary>
    /// Contains methods to transform double numbers to string.
    /// </summary>
    public static class Transform
    {
        /// <summary>
        /// Special string values for representing special double type's values.
        /// </summary>
        private static readonly List<string> SpecialValues = new List<string>
        {
            "NaN",
            "-Infinity",
            "Infinity"
        };

        /// <summary>
        /// Dictionary that contains string representations for digits and signs.
        /// </summary>
        private static readonly Dictionary<char, string> Representations = new Dictionary<char, string>()
        {
            { '0', "zero" },
            { '1', "one" },
            { '2', "two" },
            { '3', "three" },
            { '4', "four" },
            { '5', "five" },
            { '6', "six" },
            { '7', "seven" },
            { '8', "eight" },
            { '9', "nine" },
            { '.', "point" },
            { 'E', "exponenta" },
            { '-', "minus" },
            { '+', "plus" },
        };

        /// <summary>
        /// Method transforms double number to words (one word per digit).
        /// </summary>
        /// <param name="number">Double number.</param>
        /// <returns>String that represents given number.</returns>
        public static string TransformToWords(this double number)
        {
            string numberValue = Convert.ToString(number, System.Globalization.CultureInfo.InvariantCulture);
            if (IsSpecialDoubleValue(numberValue))
            {
                return numberValue;
            }

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < numberValue.Length - 1; i++)
            {
                stringBuilder.Append($"{Representations[numberValue[i]]} ");
            }

            stringBuilder.Append(Representations[numberValue[numberValue.Length - 1]]);
            return stringBuilder.ToString();
        }

        private static bool IsSpecialDoubleValue(string numberRepresentation)
        {
            foreach (var value in SpecialValues)
            {
                if (value == numberRepresentation)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
