using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entities
{
    public interface IEntity
    {
        void Persist();
        void Delete();
        void Select();

    }
}
