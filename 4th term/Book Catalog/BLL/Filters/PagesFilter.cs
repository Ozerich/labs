using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public class PagesFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            Dictionary<string, int> range = (Dictionary<string, int>)(Options);

            return book.PagesCount >= range["min"] && book.PagesCount <= range["max"];
        }
    }
}
