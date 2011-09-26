using System;
using System.Collections.Generic;

namespace Lab1_Ozierski
{
    public static class Market
    {
        private static Dictionary<ValuteType, decimal> BuyRates = new Dictionary<ValuteType, decimal>();
        private static Dictionary<ValuteType, decimal> SellRates = new Dictionary<ValuteType, decimal>();

        static Market()
        {
            BuyRates[ValuteType.USD] = 1;
            BuyRates[ValuteType.EUR] = 0.7M;
            BuyRates[ValuteType.BLR] = 1 / 7100M;
            SellRates[ValuteType.USD] = 1;
            SellRates[ValuteType.EUR] = 1.4M;
            SellRates[ValuteType.BLR] = 7900;
        }

        public static decimal GetRatio(ValuteType from, ValuteType to)
        {
            if (from == to)
                return 1;
            return SellRates[to] * BuyRates[from];
        }
    }
}
