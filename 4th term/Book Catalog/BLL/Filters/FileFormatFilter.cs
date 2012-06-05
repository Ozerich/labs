using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace BLL
{
    public class FileFormatFilter : BaseFilter
    {
        public override bool Check(Book book)
        {
            return book.FileFormat == (EnumFileFormat)Options;
        }
    }
}
