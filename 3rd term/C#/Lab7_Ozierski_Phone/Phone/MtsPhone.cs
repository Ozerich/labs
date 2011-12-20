using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public class MtsPhone : Phone
    {
        public MtsPhone(string phone_number, string phone_user) : base(phone_number, phone_user) { }
        public override string Provider
        {
            get
            {
                return "Mobile Telephone Systems";
            }
        }
    }
}
