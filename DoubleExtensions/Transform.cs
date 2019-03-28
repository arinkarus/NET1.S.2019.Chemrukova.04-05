using System;
using System.Collections.Generic;
using System.Text;

namespace DoubleExtensions
{
    public static class Transform
    {
        
        private static readonly List<string> specialValues = new List<string>
        {
            "NaN",
            "-Infinity",
            "Infinity"
        };

        private static readonly Dictionary<char, string> representations = new Dictionary<char, string>()
        {
            { '0', "zero" },
            { '1', "one" },
            { '2', "two"},
            { '3', "three" },
            { '4', "four" },
            { '5', "five" },
            { '6', "six" },
            { '7', "seven"},
            { '8', "eight" },
            { '9', "nine" },
            { '.', "point" },
            { 'E', "exponenta" },
            { '-', "minus" },
            { '+', "plus" },
        };

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
                stringBuilder.Append($"{representations[numberValue[i]]} ");
            }

            stringBuilder.Append(representations[numberValue[numberValue.Length - 1]]);
            return stringBuilder.ToString();
        }

        private static bool IsSpecialDoubleValue(string numberRepresentation)
        {
            foreach (var value in specialValues)
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
