using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsLib;
using DAL.StorageBackends;

namespace SerializeControl
{
    class Control
    {
        public void Store<S>(object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            ContactList cl = new ContactList(); //замена вместо this при переносе из класса ContactList в другую библиотеку
            sb.Store(cl, descriptor);
        }

        public static ContactList Load<S>(object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            return sb.Load(descriptor);
        }
    }
}