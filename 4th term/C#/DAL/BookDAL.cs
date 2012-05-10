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

            int id = 1;
            foreach (XElement cat in doc.Root.Elements())
                id = Math.Max(id, Int32.Parse(cat.Attribute("ID").Value));

            doc.Root.Add(new XElement("Category", new XAttribute("ID", id), new XElement("Name", name)));
            doc.Save(UserDal.fileName);
        }

        public static void CreateBook(Guid parentId, Book book)
        {
            XDocument doc = XDocument.Load(UserDal.fileName);

            int catId = 0;
            int bookId = 1;

            foreach (XElement category in doc.Root.Elements())
                foreach (XElement bookElem in category.Elements())
                    bookId = Math.Max(bookId, Int32.Parse(bookElem.Attribute("ID").Value));
            bookId++;

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
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach(XElement category in doc.Root.Elements())
                if (category.Attribute("ID").Value == Id.ToString())
                {
                    category.SetElementValue("Name", cat.Name);
                    break;
                }

            doc.Save(BookDal.fileName);
        }

        public void UpdateBook(Guid Id, Book book)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach(XElement category in doc.Root.Elements())
                foreach (XElement bookElem in category.Elements())
                    if (bookElem.Attribute("ID").Value == Id.ToString())
                    {
                        bookElem.SetElementValue("Title", book.Title);
                        bookElem.SetElementValue("Year", book.Year);
                        bookElem.SetElementValue("Pages", book.PagesCount);
                        bookElem.SetElementValue("Format", book.FileFormat);
                        bookElem.SetElementValue("Publication", book.Publication);
                        bookElem.SetElementValue("Author", book.Author);
                    }

            doc.Save(BookDal.fileName);
        }

        public static List<BookCategory> GetCategories()
        {
            XDocument doc = XDocument.Load(BookDal.fileName);
            List<BookCategory> result = new List<BookCategory>();

            foreach (XElement category in doc.Root.Elements())
                result.Add(new BookCategory(Int32.Parse(category.Attribute("ID").Value), category.Element("Name").Value));

            return result;
        }
    }
}
