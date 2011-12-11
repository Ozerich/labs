using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;


namespace Lab2_Ozierski_Train.Serialization
{
    public class XmlSerialization : ISerialization
    {        
        private XmlSerializer xs;

        public XmlSerialization()
        {
            xs = new XmlSerializer(typeof(Train));
        }

        public void Serialize(System.IO.Stream stream, Train train)
        {
            xs.Serialize(stream, train);
        }

        public Train Deserialize(System.IO.Stream stream)
        {
            return (Train)xs.Deserialize(stream);
        }
    }
}
