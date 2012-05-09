using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Entities;

namespace DAL
{
    public class BookDal
    {
        public static string fileName = "data/books.xml";

        static BookDal()
        {
            if (File.Exists(BookDal.fileName) == false)
            {
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                doc.AddFirst(new XElement("Categories"));
                doc.Save(BookDal.fileName);
            }
        }

        public static void CreateCategory(string name)
        {
            XDocument doc = XDocument.Load(UserDal.fileName);

            doc.Root.Add(new XElement("Category", new XElement("Name", name)));
            doc.Save(UserDal.fileName);
        }

        public static void CreateBook(Guid parentId, Book book)
        {
            XDocument doc = XDocument.Load(UserDal.fileName);

            int catId = 0;

            foreach (XElement category in doc.Root.Elements())
            {
                catId = Int32.Parse(category.Attribute("ID").Value);

                if (catId != Int32.Parse(parentId.ToString()))
                    continue;

                category.Add(new XElement("Book", 
                    new XElement("Title", book.Title),
                    new XElement("Author", book.Author),
                    new XElement("Publication", book.Publication),
                    new XElement("Year", book.Year),
                    new XElement("Format", book.FileFormat),
                    new XElement("Pages", book.PagesCount)
                    ));
              
                break;
            }

            doc.Save(UserDal.fileName);
        }

        public void DeleteCategory(Guid Id)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);
            int catId = 0;

            foreach (XElement category in doc.Root.Elements())
            {
                catId = Int32.Parse(category.Attribute("ID").Value);

                if (catId != Int32.Parse(Id.ToString()))
                    continue;

                category.Remove();
            }
        

            doc.Save(BookDal.fileName);
        }


        public void DeleteBook(Guid Id)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach(XElement category in doc.Root.Elements())
                foreach(XElement book in category.Elements())
                    if (book.Attribute("ID").Value == Id.ToString())
                    {
                        book.Remove();
                        break;
                    }

            doc.Save(BookDal.fileName);
        }

        public void UpdateCategory(Guid Id, BookCategory cat)
        {
            
        }

        public void UpdateBook(Guid Id, Book book)
        {

        }
    }
}
