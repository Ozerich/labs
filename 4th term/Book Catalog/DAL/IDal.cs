using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IDal
    {
        void Update(object obj);
        void Insert(object obj);
        void Delete(int id);
        object Select(int id);
    }
}
