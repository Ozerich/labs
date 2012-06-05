using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsLib.Entities;
using System.Runtime.Serialization;
using System.Collections;

namespace ContactsLib
{
    [DataContract]
    public class ContactList : IEnumerable<Contact>
    {
        [DataMember]
        public List<Contact> Contacts { get; private set; }

        public IEnumerable<Contact> Sorted
        {
            get
            {
                List<Contact> ll = new List<Contact>();
                ll.AddRange(Contacts.Select(i => i));
                ll.Sort();
                return ll;
            }
        }

        public IEnumerable<ContactGroup> Groups
        {
            get
            {
                IEnumerable<int> groups = Contacts.Select<Contact, int>(c => c.Group.Id).Distinct<int>();

                List<ContactGroup> res = new List<ContactGroup>();
                foreach (int groupId in groups)
                {
                    ContactGroup g = new ContactGroup(groupId);
                    res.Add(g);
                }
                return res;
            }
        }

        public ContactList()
        {
            Contacts = Contact.SelectAll();
        }

        public void Add(Contact c)
        {
            Contacts.Add(c);
        }

        public Contact this[int idx]
        {
            get
            {
                return Contacts[idx];
            }
        }

        public IEnumerator<Contact> GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }

        public void Remove(Contact contact)
        {
            Contacts.Remove(contact);
            contact.Delete();
        }


    }
}
