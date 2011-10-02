using System;
using System.Collections.Generic;

namespace Lab1_Ozierski
{
    public static class Market
    {
        private static Dictionary<CurrencyType, decimal> BuyRates = new Dictionary<CurrencyType, decimal>();
        private static Dictionary<CurrencyType, decimal> SellRates = new Dictionary<CurrencyType, decimal>();

        static Market()
        {
            BuyRates[CurrencyType.USD] = 1;
            BuyRates[CurrencyType.EUR] = 0.7M;
            BuyRates[CurrencyType.BLR] = 1 / 7100M;
            SellRates[CurrencyType.USD] = 1;
            SellRates[CurrencyType.EUR] = 1.4M;
            SellRates[CurrencyType.BLR] = 7900;
        }
        /// <summary>
        /// Get exchange rate
        /// </summary>
        /// <param name="from">Currency for selling</param>
        /// <param name="to">Currency for buying</param>
        /// <returns>Exchange rate</returns>
        public static decimal GetRatio(CurrencyType from, CurrencyType to)
        {
            if (from == to)
                return 1;
            return SellRates[to] * BuyRates[from];
        }
    }
}
