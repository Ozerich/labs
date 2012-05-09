using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Entities;

namespace DAL
{
    public class UserDal
    {
        public static string fileName = "data/users.xml";

        static UserDal()
        {
            if (File.Exists(UserDal.fileName) == false)
            {
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                doc.AddFirst(new XElement("users"));
                doc.Save(UserDal.fileName);
            }
        }

        public static void CreateUser(string login, string password)
        {
            XDocument doc = XDocument.Load(UserDal.fileName);

            doc.Root.Add(new XElement("User", new XElement("Login", login), new XElement("Password", password)));
            doc.Save(UserDal.fileName);
        }

        public static User FindByLogin(string login)
        {
            XDocument doc = XDocument.Load(UserDal.fileName);
            bool found = false;
            string pass = "";

            foreach (XElement userElem in doc.Root.Elements())
            {
                foreach (XElement elem in userElem.Elements())
                {
                    if (elem.Name == "Login" && elem.Value == login)
                        found = true;
                    else if (elem.Name == "Password")
                        pass = elem.Value;
                }

                if (found)
                    return new User(login, pass);
            }

            return null;
        }



        public void UpdateUser(User user)
        {

        }

        public User GetUser(Guid user)
        {
            return new User("","");
        }

        public void DeleteUser(User user)
        {

        }

        public static Dictionary<string, string> GetUsers()
        {
            Dictionary<string, string> users = new Dictionary<string, string>();


            return users;
        }
    }
}
