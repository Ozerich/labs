using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Ozierski_Rational
{
    class Program
    {
        static void Main(string[] args)
        {
            RationalNumber a, b;
            try
            {
                Console.WriteLine("Rational number A:");
                a = RationalNumber.Parse(Console.ReadLine());
                Console.WriteLine("Rational number B:");
                b = RationalNumber.Parse(Console.ReadLine());

                Console.WriteLine("\nAfter parsing: ");
                Console.WriteLine("A: " + a);
                Console.WriteLine("B: " + b);

                a = a.Short();
                b = b.Short();
                Console.WriteLine("\nAfter short: ");
                Console.WriteLine("A: " + a);
                Console.WriteLine("B: " + b);

                Console.WriteLine("A + B = " + (a + b));
                Console.WriteLine("A - B = " + (a - b));
                Console.WriteLine("A * B = " + (a * b));
                Console.WriteLine("A / B = " + (a / b));
                Console.WriteLine("A = B " + (a == b).ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message + " Try Again!");
            }
            Console.ReadLine();
        }
    }
}
