using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5_Ozierski_PluginApplication
{
    [PluginName("Substraction")]
    public class SubstractPlugin : IPlugin
    {
        public string Author
        {
            get
            {
                return "Vital Ozierski";
            }
        }

        public LongNumber DoOp(LongNumber number1, LongNumber number2)
        {
            LongNumber res = number1;
            int ost = 0, sum;
            int i = 0;
            for (; i < Math.Max(number2.num_len, number1.num_len); i++)
            {
                if(number1[i] - ost < number2[i])
                {
                    sum = 10 + number1[i] - ost - number2[i];
                    ost = 1;
                }
                else
                {
                    sum = number1[i] - number2[i] - ost;
                    ost = 0;
                }
                res[i] = sum;
            }
            return res;
        }
    }
}
