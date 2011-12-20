using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public class PhoneCreator
    {
        public Phone CreatePhone(string phone_number, string user_name)
        {
            string code = phone_number.Substring(phone_number[0] == '+' ? 1 : 0, 3);
            string number = phone_number.Substring(3);

            if (code == "029")
            {
                if (number[0] == '6' || number[0] == '3')
                    return new VelcomPhone(phone_number, user_name);
                else if (number[0] == '5' || number[0] == '7')
                    return new MtsPhone(phone_number, user_name);
            }
            else if (code == "044")
            {
                if (number[0] == '5' || number[0] == '7')
                    return new VelcomPhone(phone_number, user_name);
                else if (number[0] == '3' || number[0] == '6')
                    return new MtsPhone(phone_number, user_name);
            }
            else if (code == "025")
            {
                if (number[0] == '2')
                    return new LifePhone(phone_number, user_name);
            }

            return null;
        }
    }
}
