using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactsLib.Entities
{
    public class ContactGroup : IEnumerable<Contact>
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                foreach (Contact c in this)
                    c.Group = value;
            }
        }

        internal List<Contact> Contacts = new List<Contact>();

        internal ContactGroup(string name)
        {
            this.name = name;
        }

        public IEnumerator<Contact> GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }
    }
}
