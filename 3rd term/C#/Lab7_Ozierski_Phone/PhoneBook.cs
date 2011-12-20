using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Lab7_Ozierski_Phone
{
    public class PhoneBook : List<Phone>
    {

        public void SaveToTextfile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);

            StreamWriter sw = new StreamWriter(fs);
            
            sw.WriteLine(Count);

            foreach (Phone phone in this)
            {
                sw.WriteLine(phone.Id);
                sw.WriteLine(phone.PhoneNumber);
                sw.WriteLine(phone.User);
            }

            sw.Close();
            fs.Close();
        }

        public void SaveToBinaryFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Count);

            foreach (Phone phone in this)
            {
                bw.Write(phone.Id);
                bw.Write(phone.PhoneNumber);
                bw.Write(phone.User);
            }

            bw.Close();
            fs.Close();
        }

        public void SaveToXmlFile(string filename)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("phonebook");

            foreach (Phone phone in this)
            {
                XmlElement phone_xml = xml.CreateElement("Phone");

                XmlAttribute id = xml.CreateAttribute("Id");
                id.Value = phone.Id.ToString();

                XmlAttribute phoneNumber = xml.CreateAttribute("PhoneNumber");
                phoneNumber.Value = phone.PhoneNumber;

                XmlAttribute user = xml.CreateAttribute("User");
                user.Value = phone.User;

                phone_xml.Attributes.Append(id);
                phone_xml.Attributes.Append(phoneNumber);
                phone_xml.Attributes.Append(user);

                root.AppendChild(phone_xml);
            }

            xml.AppendChild(root);

            XDocument xdoc = XDocument.Parse(xml.OuterXml);

            TextWriter tw = new StreamWriter(filename);
            tw.WriteLine(xdoc.ToString());
            tw.Close();           
        }

        public void LoadFromTextfile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            Clear();

            PhoneCreator creator = new PhoneCreator();

            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader st = new StreamReader(fs);

            int count = Int32.Parse(st.ReadLine());

            for (int i = 0; i < count; i++)
            {
                int id = Int32.Parse(st.ReadLine());
                Add(creator.CreatePhone(st.ReadLine(), st.ReadLine()));

                IdGenerator.Instance.Last = Math.Max(IdGenerator.Instance.Last, id + 1);
            }

            st.Close();
            fs.Close();
        }

        public void LoadFromBinaryFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            Clear();

            PhoneCreator creator = new PhoneCreator();

            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            int count = br.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                int id = br.ReadInt32();
                Add(creator.CreatePhone(br.ReadString(), br.ReadString()));

                IdGenerator.Instance.Last = Math.Max(IdGenerator.Instance.Last, id + 1);
            }

            br.Close();
            fs.Close();
        }

        public void LoadFromXmlFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            Clear();

            PhoneCreator creator = new PhoneCreator();

            XmlDocument xml = new XmlDocument();
            xml.Load(filename);

            XmlElement root = xml.FirstChild as XmlElement;
            
            foreach(XmlElement elem in root.ChildNodes)
            {
                Add(creator.CreatePhone(elem.Attributes["PhoneNumber"].Value, elem.Attributes["User"].Value));
                IdGenerator.Instance.Last = Math.Max(IdGenerator.Instance.Last, Int32.Parse(elem.Attributes["Id"].Value) + 1);
            }

        }
    }
}
