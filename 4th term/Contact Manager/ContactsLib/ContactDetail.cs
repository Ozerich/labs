using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ContactsLib
{
    [Table(Name="ContactDetail")]
    public class ContactDetail
    {
        [Column(IsDbGenerated=true, IsPrimaryKey = true)]
        public int ID { get; set; }

        [Column]
        public int Contact_ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Value { get; set; }
    }
}
