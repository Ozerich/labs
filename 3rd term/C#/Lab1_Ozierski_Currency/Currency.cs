using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1_Ozierski
{
    public enum ValuteType {USD, BLR, EUR};
    class Currency: IComparable<Currency>, IEquatable<Currency>
    {
        public decimal Money { get; private set; }
        public ValuteType Valute { get; private set; }

        public Currency(decimal money, ValuteType valute)
        {
            Money = money;
            Valute = valute;
        }

        public int CompareTo(Currency other)
        {
            return Money.CompareTo(other);
        }

        public bool Equals(Currency other)
        {
            return CompareTo(other) == 0 && other.Valute == Valute;
        }

        public override bool Equals(object b)
        {
            if (!(b is Currency))
                return false;
            return CompareTo((Currency)b) == 0;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0:0.00} {1}", Money, Valute);
        }

        public static Currency operator +(Currency a, Currency b)
        {
            return new Currency(a.Money + b.Convert(a.Valute).Money, a.Valute);
        }

        public static Currency operator -(Currency a, Currency b)
        {
            return new Currency(a.Money - b.Convert(a.Valute).Money, a.Valute);
        }

        public static Currency operator *(Currency a, decimal b)
        {
            return new Currency(a.Money * b, a.Valute);
        }

        public static Currency operator /(Currency a, decimal b)
        {
            return new Currency(a.Money / b, a.Valute);
        }

        public static Currency operator +(Currency a, decimal b)
        {
            return new Currency(a.Money + b, a.Valute);
        }

        public static Currency operator -(Currency a, decimal b)
        {
            return new Currency(a.Money - b, a.Valute);
        }

        public static bool operator ==(Currency a, Currency b)
        {
            if (a == null || b == null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Currency a, Currency b)
        {
            return !(a == b);
        }

        public static Currency Parse(string s)
        {
            Regex regex = new Regex(@"(\d+)(\.\d*\d*)*\s*(\w{3})");
            if (regex.IsMatch(s))
            {
                string[] data = regex.Split(s);
                if (data.Length == 4)
                    return new Currency(Decimal.Parse(data[1]), (ValuteType)Enum.Parse(typeof(ValuteType), data[2]));
                else if (data.Length == 5)
                    return new Currency(Decimal.Parse(data[1] + data[2]), (ValuteType)Enum.Parse(typeof(ValuteType), data[3]));
                else
                    throw new FormatException("Invalid format");
            }
            else
                throw new FormatException("Invalid format");
        }

        public Currency Convert(ValuteType to)
        {
            return new Currency(Money * Market.GetRatio(Valute, to), to);
        }

    }
}
