using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ContactsApp
{
    class Sample
    {
        private string date = "2012";

        public Sample()
        {
        }

        public Sample(string date)
        {
            this.date = date;
        }

        public override string ToString()
        {
            return date.ToString();
        }
    }
}
