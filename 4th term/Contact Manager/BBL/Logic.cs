using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsLib;
using ContactsLib.Entities;
using DAL.StorageBackends;
using SerializeControl;

namespace BBL
{
    public class Logic
    {
        public static ContactList CreateContactList()
        {
            ContactList cl = new ContactList();
            return cl;
        }

        public static ContactList LoadContactList(string filename)
        {
            ContactList cl = Controls.Load<XMLStorageBackend>(filename);
            return cl;
        }

        public static void SaveContactList(ContactList cl, string filename)
        {
            Controls.Store<XMLStorageBackend>(cl, filename);
        }
    }
}
