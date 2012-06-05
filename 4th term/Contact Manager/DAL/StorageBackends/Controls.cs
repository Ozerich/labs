using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsLib;
using DAL.StorageBackends;

namespace SerializeControl
{
     public class Controls
    {
        public static void Store<S>(ContactList cl, object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            sb.Store(cl, descriptor);
        }

        public static ContactList Load<S>(object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            return sb.Load(descriptor);
        }
    }
}