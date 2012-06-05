using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ContactsLib.Entities
{
    [DataContract]
    [KnownType(typeof(SimpleContactDetail))]
    public abstract class ContactDetail
    {
        [DataMember]
        public String Name;

        [DataMember]
        public abstract object Content { get; set; }
    }
}
