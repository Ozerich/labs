using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Serialization
{
    public interface ISerialization
    {
        void Serialize(Stream stream, Train train);
        Train Deserialize(Stream stream);
    }
}
