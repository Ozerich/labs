using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ContactsLib.Entities
{
    [DataContract]
    public class SimpleContactDetail : ContactDetail
    {
        public override object Content
        {
            get;
            set;
        }

        public SimpleContactDetail(string name, object data)
        {
            Name = name;
            Content = data;
        }
    }
}
