using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Book
    {
        
        private int year;
        private int pagesCount;

        /// <summary>
        /// Book Identificator
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        public List<string> Tags { get; set; } 

        /// <summary>
        /// Book title
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Book publish year
        /// </summary>
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

        /// <summary>
        /// Book pages count
        /// </summary>
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

        /// <summary>
        /// File format (PDF, DJVU, etc.)
        /// </summary>
        public EnumFileFormat FileFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Author Name
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Publication Information
        /// </summary>
        public string Publication
        {
            get;
            set;
        }


        public string Genre
        {
            get;
            set;
        }

    }
}
