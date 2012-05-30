using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Entities
{
    [Table(Name = "Tags")]
    public class BookTag
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int Book_Id { get; set; }

        [Column(Name="Tag")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
