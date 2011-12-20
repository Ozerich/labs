using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public class VelcomPhone : Phone
    {
        public VelcomPhone(string phone_number, string phone_user) : base(phone_number, phone_user) { }
        public override string Provider
        {
            get
            {
                return "Velcom";
            }
        }
    }
}
