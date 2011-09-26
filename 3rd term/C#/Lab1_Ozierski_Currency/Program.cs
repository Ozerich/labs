using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Ozierski
{
    class Program
    {
        static void Main(string[] args)
        {
            Currency dollars = Currency.Parse("50 USD");
            Currency euros = Currency.Parse("40.50 EUR");
            Currency blrs = Currency.Parse("100000BLR");

            Console.WriteLine("Dollars: " + dollars.ToString());
            Console.WriteLine("Euro: " + euros.ToString());
            Console.WriteLine("BLR: " + blrs.ToString());

            Console.WriteLine("\nConvert from BLR to Dollars: ");
            Console.WriteLine(blrs.Convert(ValuteType.USD).ToString());

            Console.WriteLine("\nConvert from EUR to Dollars: ");
            Console.WriteLine(euros.Convert(ValuteType.USD).ToString());

            Console.WriteLine("\nConvert from EUR to BLR: ");
            Console.WriteLine(euros.Convert(ValuteType.BLR).ToString());

            Console.WriteLine("\nUSD + EUR: ");
            Console.WriteLine((dollars + euros).ToString());

            Console.WriteLine("\nEUR + 50:");
            Console.WriteLine((euros + 50).ToString());

            

            Console.Read();
        }
    }
}
