using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public class IdGenerator : Singleton<IdGenerator>
    {
        private int next;

        private IdGenerator()
        {
            next = 1;
        }

        public int Get()
        {
            return next++;
        }

        public int Last
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }
    }
}
