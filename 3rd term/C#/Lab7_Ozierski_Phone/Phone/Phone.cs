using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public abstract class Phone
    {
        public string Code { get; set; }
        public string Number { get; set; }
        public string User { get; set; }
        public int Id { get; set; }

        public abstract string Provider { get; }

        public string PhoneNumber
        {
            get
            {
                return String.Format("{0}{1}", Code, Number);
            }
        }

        public Phone() { }
        public Phone(string phone_number, string phone_user)
        {
            Code = phone_number.Substring(phone_number[0] == '+' ? 1 : 0, 3);
            Number = phone_number.Substring(3);

            User = phone_user;

            Id = IdGenerator.Instance.Get();
        }

        public override string ToString()
        {
            return String.Format("{0}. {1}: {2} - {3}", Id, Provider, Number, User);
        }
    }
}
