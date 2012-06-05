using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace ContactsLib
{
    public class ContactList : IEnumerable<Contact>
    {
        public List<Contact> Contacts { get; private set; }

        public IEnumerable<Contact> Sorted
        {
            get
            {
                List<Contact> ll = new List<Contact>();
                ll.AddRange(Contacts.Select(i => i));
                return ll;
            }
        }

        public IEnumerable<ContactGroup> Groups
        {
            get
            {
                Catalog catalog = new Catalog();
                IEnumerable<int> groups = Contacts.Select<Contact, int>(c => c.Group_ID).Distinct<int>();

                List<ContactGroup> res = new List<ContactGroup>();
                foreach (int groupId in groups)
                {
                    var query = from contactGroup in catalog.ContactGroups where contactGroup.ID == groupId select contactGroup;
                    if(query.Count() > 0)
                        res.Add(query.First());
                    else
                    {
                        ContactGroup cg = new ContactGroup();
                        cg.Name = "Other";
                        cg.ID = 0;
                        res.Add(cg);
                    }
                }
                return res;
            }
        }

        public ContactList()
        {
            Catalog catalog = new Catalog();
            var query = from contact in catalog.Contacts select contact;
            Contacts = query.ToList();
        }

        public void Add(Contact c)
        {
            Contacts.Add(c);

            Catalog catalog = new Catalog();
            catalog.Contacts.InsertOnSubmit(c);

            catalog.SubmitChanges();
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

        public void RemoveDetail(ContactDetail cd)
        {
            Catalog catalog = new Catalog();

            catalog.ContactDetails.Attach(cd);
            catalog.ContactDetails.DeleteOnSubmit(cd);
            catalog.SubmitChanges();
        }

        public void Remove(Contact contact)
        {
            Contacts.Remove(contact);

            Catalog catalog = new Catalog();
            var query = from contactq in catalog.Contacts where contactq.ID == contact.ID select contactq;

            catalog.Contacts.DeleteOnSubmit(query.First());

            catalog.SubmitChanges();
        }

        public static ContactGroup FindGroupById(int Id)
        {
            if (Id == 0)
                return new ContactGroup() { Name = "Other" };
            Catalog catalog = new Catalog();

            var query = from catalogGroup in catalog.ContactGroups where catalogGroup.ID == Id select catalogGroup;
            return query.First();
        }

        public static ContactGroup GetGroupByName(string name)
        {
            Catalog catalog = new Catalog();

            var query = from catalogGroup in catalog.ContactGroups where catalogGroup.Name == name select catalogGroup;
            if (query.Count() == 0 && name != "Other")
            {
                ContactGroup cg = new ContactGroup() { Name = name };
                catalog.ContactGroups.InsertOnSubmit(cg);
                catalog.SubmitChanges();
                return cg;
            }
            else
                return query.First();
        }

        public static void UpdateContact(Contact contact)
        {
            Catalog catalog = new Catalog();

            var query = from contactDb in catalog.Contacts where contactDb == contact select contactDb;

            Contact c = query.First();
            
            c.Name = contact.Name;
            c.Group_ID = contact.Group_ID;
            c.Details = contact.Details;

            catalog.SubmitChanges();
        }

        public static void UpdateContactDetail(ContactDetail cd)
        {
            Catalog catalog = new Catalog();

            var query = from contactDb in catalog.ContactDetails where contactDb.ID == cd.ID select contactDb;

            if (query.Count() != 0)
            {
                ContactDetail c = query.First();
                c.Name = cd.Name;
                c.Value = cd.Value;
                c.Contact_ID = cd.Contact_ID;
            }
            else
                catalog.ContactDetails.InsertOnSubmit(cd);

            catalog.SubmitChanges();
        }


    }
}
