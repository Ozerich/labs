using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5_Ozierski_PluginApplication
{
    public class LongNumber
    {
        public int[] num_data;
        public int num_len;

        public int this[int index]
        {
            get
            {
                return num_data[index];
            }
            set
            {
                num_data[index] = value;
                if (index >= num_len && value != 0)
                    num_len = index + 1;
            }
        }

        public LongNumber()
        {
            num_data = new int[1000];
        }

        public LongNumber(int num)
        {
            num_data = new int[1000];
            num_len = 0;
            while (num > 0)
            {
                num_data[num_len++] = num % 10;
                num /= 10;
            }
        }

        public LongNumber(string num)
        {
            num_data = new int[1000];
            for (int i = num.Length - 1, j = 0; i >= 0; i--)
            {
                if (num[i] >= '0' && num[i] <= '9')
                    num_data[j++] = num[i] - '0';
                else throw new FormatException();
            }
            num_len = num.Length;
        }

        public override string ToString()
        {
            string res = "";
            int i = num_len;
            while (--i > 0 && num_data[i] == 0) continue;
            for (; i >= 0; i--)
                res += num_data[i];
            return res;
        }
    }
}
