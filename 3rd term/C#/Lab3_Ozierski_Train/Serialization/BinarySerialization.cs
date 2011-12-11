using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab2_Ozierski_Train.Serialization
{
    public class BinarySerialization : ISerialization
    {
        private BinaryFormatter bf;

        public BinarySerialization()
        {
            bf = new BinaryFormatter();
        }

        public void Serialize(Stream stream, Train train)
        {
            bf.Serialize(stream, train);
        }

        public Train Deserialize(Stream stream)
        {
            return (Train)bf.Deserialize(stream);
        }
    }
}
