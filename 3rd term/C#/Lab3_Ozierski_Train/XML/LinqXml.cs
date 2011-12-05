using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace Lab2_Ozierski_Train.XML
{
    public class LinqXml : BaseXML<XDocument, XElement>
    {

        public override XDocument SaveTrain(Train t)
        {
            XDeclaration declaration = new XDeclaration("1.0", UTF8Encoding.UTF8.ToString(), "yes");
            return new XDocument(declaration,
                new XElement("Train", new XAttribute("Number", t.Number.ToString()),
                    from coach in t select SaveCoach(coach)));
        }

        public override Train LoadTrain(XDocument xml)
        {
            Train t = new Train(Int32.Parse(xml.Root.Attribute("Number").Value));

            foreach (XElement elem in xml.Elements())
                t.AddCoach(LoadCoach(elem));

            return t;
        }

        protected override XElement SaveCoach(Coach c)
        {
            XElement coach = new XElement("Coach");
            coach.Add(new XAttribute("id", c.Id));
            coach.Add(new XAttribute("type", (Int32)c.Type));
            
            coach.Add(from r in c select SaveRoom(r));

            return coach;
        }

        protected override Coach LoadCoach(XElement xml)
        {
            Coach coach = new Coach(Int32.Parse(xml.Attribute("id").Value), (CoachType)Int32.Parse(xml.Attribute("type").Value));
            
            foreach (XElement elem in xml.Elements())
                coach.AddRoom(LoadRoom(elem));
            
            return coach;
        }

        protected override XElement SaveRoom(Room r)
        {
            return new XElement("Room", new XAttribute("limit", r.PassengerLimit), new XAttribute("count", r.PassengerCount));
        }

        protected override Room LoadRoom(XElement xml)
        {
            return new Room(Int32.Parse(xml.Attribute("limit").Value), Int32.Parse(xml.Attribute("count").Value));
        }
    }
}
