using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public class PublicationFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            string find_str = (string)Options;
            return book.Author.Contains(find_str);
        }
    }
}
