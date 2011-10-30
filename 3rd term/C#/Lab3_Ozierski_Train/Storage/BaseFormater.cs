using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage
{
    public abstract class BaseFormater
    {
        public abstract void WriteTrain(Train train, Stream stream);
        protected abstract void WriteCoach(Coach coach);
        protected abstract void WriteRoom(Room room);

        public abstract Train ReadTrain(Stream stream);
        protected abstract Coach ReadCoach();
        protected abstract Room ReadRoom();
    }
}
