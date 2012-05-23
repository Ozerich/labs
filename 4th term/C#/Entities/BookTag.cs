using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class BookTag
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
