using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public class TagFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            foreach (string tag in book.Tags)
                if (Options.Equals(tag))
                    return true;
            return false;
        }
    }
}
