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
            return book.Author.Contains((string)Options);
        }
    }
}
