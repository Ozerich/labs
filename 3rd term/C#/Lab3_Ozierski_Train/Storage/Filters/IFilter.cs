using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage.Filters
{
    public enum FilterMode
    {
        Write = 1,
        Read = 2
    }

    public interface IFilter
    {
        Stream Apply(Stream stream, FilterMode fm);
    }
}
