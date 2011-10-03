using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Ozierski_Rational
{
    static class MyMath
    {
		/// <summary>
		/// Great common divisor
		/// </summary>
		/// <param name="a">
		/// first number
		/// </param>
		/// <param name="b">
		/// second number
		/// </param>
        public static int GCD(int a, int b)
        {
            while (a > 0 && b > 0)
                if (a > b)
                    a = a % b;
                else
                    b = b % a;
            return Math.Max(a, b);
        }
        /// <summary>
        /// Least common multiple
        /// </summary>
        /// <param name="a">
        /// first number
        /// </param>
        /// <param name="b">
        /// second number
        /// </param>
        public static int LCM(int a, int b)
        {
            return a * b / GCD(a, b);             
        }
    }
}
