using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class Polynomial
    {
        public readonly double number;

        private readonly int[] coefficients;

        public int[] Coefficients
        {
            get
            {
                return this.coefficients;
            }
        }

        public double Number
        {
            get
            {
                return this.number;
            }
        }

        public Polynomial(double number, int[] coefficients)
        {
            this.number = number;
            this.coefficients = coefficients;
        }


    }
}
