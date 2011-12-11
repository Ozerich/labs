using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1_Ozierski_Rational
{
    class RationalNumber : IComparable<RationalNumber>, IEquatable<RationalNumber>
    {
        private int denominator;

        public int Numerator{private get; set;}
        public int Denominator
        {
            private get 
            {
                return denominator;
            }
            set
            {
                if (value == 0)
                    throw new FormatException("Denominator is zero");
                else
                    denominator = value;
            }
        }

        public RationalNumber()
        {
            Numerator = 0;
            Denominator = 1;
        }

        public RationalNumber(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public RationalNumber(int numerator) : this(numerator, 1) { }

        public bool Equals(RationalNumber other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if(!((object)obj is RationalNumber))
                return false;
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            return Denominator.GetHashCode() + Numerator.GetHashCode();
        }

        public int CompareTo(RationalNumber other)
        {
            int common = MyMath.LCM(Denominator, other.Denominator);
            return (Numerator * common / Denominator) - (other.Numerator * common / other.Denominator);
        }

        public static RationalNumber operator +(RationalNumber a, RationalNumber b)
        {
            int common = MyMath.LCM(a.Denominator, b.Denominator);
            return new RationalNumber(a.Numerator * common / a.Denominator + b.Numerator * common / b.Denominator, common);
        }

        public static RationalNumber operator -(RationalNumber a, RationalNumber b)
        {
            int common = MyMath.LCM(a.Denominator, b.Denominator);
            return new RationalNumber(a.Numerator * common / a.Denominator - b.Numerator * common / b.Denominator, common);
        }

        public static RationalNumber operator *(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Short();
        }

        public static RationalNumber operator /(RationalNumber a, RationalNumber b)
        {
            return new RationalNumber(a.Numerator * b.Denominator, a.Denominator * b.Numerator).Short();
        }

        public static bool operator ==(RationalNumber a, RationalNumber b)
        {
            if((object)a == null || (object)b == null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(RationalNumber a, RationalNumber b)
        {
            if ((object)a == null || (object)b == null)
                return false;
            return !(a == b);
        }

        public static bool operator >(RationalNumber a, RationalNumber b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(RationalNumber a, RationalNumber b)
        {
            return a.CompareTo(b) < 0;
        }

        public static implicit operator RationalNumber(int a)
        {
            return new RationalNumber(a);
        }

        public static implicit operator RationalNumber(double a)
        {
            string[] parts = a.ToString().Split('.');
            if(parts.Length == 2)
            {
                int pow =  (int)Math.Pow(10, parts[1].Length);
                return new RationalNumber(int.Parse(parts[0]) * pow + int.Parse(parts[1]), pow * 10);    
            }
            else
                return new RationalNumber((int)a);
        }

        public static implicit operator double(RationalNumber a)
        {
            return ((double)a.Numerator / a.Denominator);
        }

		/// <summary>
		/// Reduce fraction
		/// </summary>
		/// <returns>
		/// Reduced fraction
		/// </returns>
        public RationalNumber Short()
        {
            int gcd = MyMath.GCD(Numerator, Denominator);
            return new RationalNumber(Numerator / gcd, Denominator / gcd);
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}", Numerator, Denominator);
        }

		/// <summary>
		/// Converto to decimal format 0.00
		/// </summary>
		/// <returns>
		///	output string
		/// </returns>
        public string ToDoubleString()
        {
            return ((double)Numerator / Denominator).ToString();
        }
		
		/// <summary>
		/// Parse string
		/// </summary>
		/// <param name="str">
		/// input string
		/// </param>
        public static RationalNumber Parse(string str)
        {
            if (Regex.IsMatch(str, @"^\d+[,\.]\d+$"))
            {
                return (RationalNumber)double.Parse(str);
            }
            else if (Regex.IsMatch(str, @"^\d+[,\.]\d*\(\d+\)$"))
            {
                string[] parts = str.Split(',', '.', '(', ')');
                int pow = (int)Math.Pow(10, parts[1].Length);
                RationalNumber left = new RationalNumber(int.Parse(parts[0]) * pow + int.Parse(parts[1]), pow);
                pow = (int)Math.Pow(10, parts[2].Length);
                RationalNumber right = new RationalNumber(int.Parse(parts[2]), pow - 1);
                return left + right;

            }
            else if (Regex.IsMatch(str, @"^\d+$"))
            {
                return new RationalNumber(int.Parse(str));
            }
            else if (Regex.IsMatch(str, @"^\d+/\d+$"))
            {
                string[] parts = str.Split('/');
                return new RationalNumber(int.Parse(parts[0]), int.Parse(parts[1]));
            }
            else 
                throw new FormatException("Incorrect format");
        }
    }
}
