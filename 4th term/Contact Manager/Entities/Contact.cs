using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ContactsLib.Entities
{
    [DataContract]
    public class Contact : IComparable<Contact>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public List<ContactDetail> Details = new List<ContactDetail>();

        public Contact(string name)
        {
            Name = name;
        }

        public int CompareTo(Contact other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
