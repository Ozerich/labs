using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ContactsLib
{
    [Table(Name="ContactGroup")]
    public class ContactGroup
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
