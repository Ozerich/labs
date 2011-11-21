using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5_Ozierski_PluginApplication
{
    public class AddPlugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "Addition";
            }
        }

        public string Author
        {
            get
            {
                return "Vital Ozierski";
            }
        }

        public LongNumber DoOp(LongNumber number1, LongNumber number2)
        {
            LongNumber res = new LongNumber();
            int ost = 0, sum;
            int i = 0;
            for (; i < Math.Max(number1.num_len, number2.num_len); i++)
            {
                sum = number2[i] + number1[i] + ost;
                ost = sum / 10;
                res[i] = sum % 10;
  
            }
            if (ost > 0)
                res[i] = ost;
            return res;
        }
    }
}
