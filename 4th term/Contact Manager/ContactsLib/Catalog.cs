using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace ContactsLib
{
    public class Catalog : DataContext
    {
        public Table<Contact> Contacts;
        public Table<ContactGroup> ContactGroups;
        public Table<ContactDetail> ContactDetails;

        public Catalog() : base("Initial Catalog=MSDB2; Integrated Security=SSPI") { }
    }
}
