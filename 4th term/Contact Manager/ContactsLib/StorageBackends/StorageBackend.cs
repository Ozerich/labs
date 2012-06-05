using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactsLib.StorageBackends
{
    public abstract class StorageBackend
    {
        public StorageBackend() { }

        internal abstract void Store(ContactList list, object descriptor);
        internal abstract ContactList Load(object descriptor);
    }
}
