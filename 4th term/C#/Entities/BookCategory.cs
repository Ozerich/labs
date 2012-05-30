using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace Entities
{
    [Table(Name="Categories")]
    public class BookCategory
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int Id { get; private set; }

        [Column]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
