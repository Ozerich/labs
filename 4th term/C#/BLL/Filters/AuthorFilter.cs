using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public class AuthorFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            string author_str = (string)Options;
            return book.Author.Contains(author_str);
        }
    }
}
