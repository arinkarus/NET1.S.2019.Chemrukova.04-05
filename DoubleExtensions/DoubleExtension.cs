using System.Runtime.InteropServices;
using System.Text;
using System;

namespace DoubleExtensions
{
    /// <summary>
    /// Class that provides methods for working with double numbers.
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// Amount of bits for double representation.
        /// </summary>
        private const int AmountOfBitsForDouble = 64;

        /// <summary>
        /// Method returns binary representation for given double number
        /// in IEEE 754 format.
        /// </summary>
        /// <param name="number">Double number.</param>
        /// <returns>Binary representation that is written in string for given number.</returns>
        public static string GetIEEE754BinaryRepresentation(this double number)
        {
            var float64 = new Float64(number);
            ulong ulongValue = float64.UlongValue;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < AmountOfBitsForDouble; i++)
            {
                if ((ulongValue & 1) == 1)
                {
                    stringBuilder.Insert(0, "1");
                }
                else
                {
                    stringBuilder.Insert(0, "0");
                }

                ulongValue >>= 1;
            }

            return stringBuilder.ToString();
        }

        public static string[] TransformToWords(this double[] array)
        {
            ValidateArray(array);
            var stringRepresentations = new string[array.Length];
            for (int i = 0; i < stringRepresentations.Length; i++)
            {
                stringRepresentations[i] = array[i].TransformToWords();
            }
            return stringRepresentations;
        }

        private static void ValidateArray(double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} can't be null.");
            }
            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} can't be empty.");
            }
        }

        /// <summary>
        /// Instance of this struct holds double value and long 
        /// value in one piece of memory.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct Float64
        {
            /// <summary>
            /// Double value.
            /// </summary>
            [FieldOffset(0)]
            private readonly double doubleValue;

            /// <summary>
            /// Unsigned long value.
            /// </summary>
            [FieldOffset(0)]
            private readonly ulong ulongValue;

            /// <summary>
            /// Initializes a new instance of the <see cref="Float64"/> struct.
            /// </summary>
            /// <param name="value">Double number.</param>
            public Float64(double value)
            {
                this.ulongValue = 0;
                this.doubleValue = value;
            }

            /// <summary>
            /// Gets unsigned value 
            /// </summary>
            public ulong UlongValue
            {
                get
                {
                    return this.ulongValue;
                }
            }
        }
    }
}