using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace ContactsLib.StorageBackends
{
    public class XMLStorageBackend : StorageBackend
    {
        internal override void Store(ContactList list, object descriptor)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(descriptor as string, FileMode.Create);
            }
            catch
            {
                throw new ArgumentException("Invalid file name");
            }

            try
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(ContactList));
                dcs.WriteObject(fs, list);
            }
            finally
            {
                fs.Close();
            }
        }

        internal override ContactList Load(object descriptor)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(descriptor as string, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                throw new ArgumentException("Invalid file name");
            }

            try
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(ContactList));
                ContactList result = (ContactList)dcs.ReadObject(fs);
                return result;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
