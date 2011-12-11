using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lab2_Ozierski_Train.XML
{
    public abstract class BaseXML<D, E>
    {

        public abstract D SaveTrain(Train t);
        public abstract Train LoadTrain(D xml);

        protected abstract E SaveCoach(Coach c);
        protected abstract Coach LoadCoach(E xml);

        protected abstract E SaveRoom(Room r);
        protected abstract Room LoadRoom(E xml);

    }
}
