using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.Linq.Mapping;

namespace Entities
{
    [Table(Name="Books")]
    public class Book
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; private set; }


        private int year;
        private int pagesCount;

        [Column(Name="Category_ID")]
        public int parentId { get; set; }


        public List<string> Tags { get; set; } 


        [Column]
        public string Title
        {
            get;
            set;
        }

        [Column]
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value > DateTime.Now.Year + 1 || value <= 1000)
                    throw new ArgumentException("Year is incorrect");
                this.year = value;
            }
        }

        [Column(Name="Pages")]
        public int PagesCount
        {
            get
            {
                return pagesCount;
            }
            set
            {
                if (value < 0 || value > 10000)
                    throw new ArgumentException("Pages count is incorrect");
                this.pagesCount = value;
            }
        }

        [Column(Name="FileFormat", DbType = "VARCHAR(255) NOT NULL")]
        public EnumFileFormat FileFormat
        {
            get;
            set;
        }

        [Column]
        public string Author
        {
            get;
            set;
        }

        [Column]
        public string Publication
        {
            get;
            set;
        }

        [Column]
        public string Genre
        {
            get;
            set;
        }


     
    }
}
