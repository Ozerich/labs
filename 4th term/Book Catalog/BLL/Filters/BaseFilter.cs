using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public abstract class BaseFilter
    {
        public object Options { get; set; }

        public abstract bool Check(Book book);
    }
}
