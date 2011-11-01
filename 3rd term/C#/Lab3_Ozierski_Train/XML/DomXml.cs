using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lab2_Ozierski_Train.XML
{
    public class DomXml : BaseXML<XmlDocument, XmlElement>
    {
        private XmlDocument xml;

        public override XmlDocument SaveTrain(Train t)
        {
            xml = new XmlDocument();

            XmlElement root = xml.CreateElement("Train");
            XmlAttribute trainnum = xml.CreateAttribute("number");
            trainnum.Value = t.Number.ToString();
            root.Attributes.Append(trainnum);

            foreach (Coach coach in t)
                root.AppendChild(SaveCoach(coach));
            xml.AppendChild(root);

            return xml;
        }


        public override Train LoadTrain(XmlDocument xml)
        {
            XmlElement root = xml.FirstChild as XmlElement;

            Train t = new Train(Int32.Parse(root.Attributes["number"].Value));
            foreach (XmlElement elem in root.ChildNodes)
                t.AddCoach(LoadCoach(elem));

            return t;
        }

        protected override XmlElement SaveCoach(Coach c)
        {
            XmlElement coach = xml.CreateElement("Coach");

            XmlAttribute id = xml.CreateAttribute("id");
            id.Value = c.Id.ToString();
            coach.Attributes.Append(id);

            XmlAttribute type = xml.CreateAttribute("type");
            type.Value = ((Int32)c.Type).ToString();
            coach.Attributes.Append(type);

            foreach (Room r in c)
                coach.AppendChild(SaveRoom(r));

            return coach;
        }

        protected override Coach LoadCoach(XmlElement xml)
        {
            Coach coach = new Coach(Int32.Parse(xml.Attributes["id"].Value), (CoachType)Int32.Parse(xml.Attributes["type"].Value));

            foreach (XmlElement elem in xml.ChildNodes)
                coach.AddRoom(LoadRoom(elem));

            return coach;
        }

        protected override XmlElement SaveRoom(Room r)
        {
            XmlElement room = xml.CreateElement("Room");

            XmlAttribute limit = xml.CreateAttribute("limit");
            limit.Value = r.PassengerLimit.ToString();
            XmlAttribute count = xml.CreateAttribute("count");
            count.Value = r.PassengerCount.ToString();

            room.Attributes.Append(limit);
            room.Attributes.Append(count);

            return room;
        }

        protected override Room LoadRoom(XmlElement xml)
        {
            return new Room(Int32.Parse(xml.Attributes["limit"].Value), Int32.Parse(xml.Attributes["count"].Value));
        }
    }
}
