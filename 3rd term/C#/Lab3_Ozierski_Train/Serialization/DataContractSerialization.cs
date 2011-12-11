using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Lab2_Ozierski_Train.Serialization
{
    /*
    public class DataContractSerialization : ISerialization
    {
        private DataContractSerializer dcs;

        public DataContractSerialization()
        {
            dcs = new DataContractSerializer(typeof(Train));
        }

        public void Serialize(System.IO.Stream stream, Train train)
        {
            dcs.WriteObject(stream, train);
        }

        public Train Deserialize(System.IO.Stream stream)
        {
            return (Train)dcs.ReadObject(stream);
        }
    }
     * */
}
