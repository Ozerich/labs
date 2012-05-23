using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace DAL
{
    public class TitleFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            string title_str = (string)Options;
            return book.Title.Contains(title_str);
        }
    }
}
