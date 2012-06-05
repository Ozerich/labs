using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace ContactsLib
{
    [Table(Name="Contact")]
    public class Contact
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; private set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int Group_ID { get; set;}

        public List<ContactDetail> Details
        {
            get
            {
                Catalog catalog = new Catalog();

                var query = from detail in catalog.ContactDetails where detail.Contact_ID == ID select detail;
                return query.ToList();
            }
            set
            {
            }
        }
    }
}
