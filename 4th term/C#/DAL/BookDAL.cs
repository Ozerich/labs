﻿using System;
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
            XDocument doc = XDocument.Load(BookDal.fileName);

            int id = 0;
            foreach (XElement cat in doc.Root.Elements())
                id = Math.Max(id, Int32.Parse(cat.Attribute("ID").Value));
            id++;

            doc.Root.Add(new XElement("Category", new XAttribute("ID", id), new XElement("Name", name)));
            doc.Save(BookDal.fileName);
        }

        public static void CreateBook(int parentId, Book book)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            int catId = 0;
            int bookId = 0;

            foreach (XElement category in doc.Root.Elements())
                foreach (XElement bookElem in category.Elements("Books").Elements("Book"))
                    bookId = Math.Max(bookId, Int32.Parse(bookElem.Attribute("ID").Value));
            bookId++;

            foreach (XElement category in doc.Root.Elements())
            {
                catId = Int32.Parse(category.Attribute("ID").Value);

                if (catId != Int32.Parse(parentId.ToString()))
                    continue;

                if (category.Element("Books") == null)
                    category.Add(new XElement("Books"));

                category.Element("Books").Add(new XElement("Book", new XAttribute("ID", bookId),
                    new XElement("Title", book.Title),
                    new XElement("Author", book.Author),
                    new XElement("Publication", book.Publication),
                    new XElement("Year", book.Year),
                    new XElement("Format", book.FileFormat),
                    new XElement("Pages", book.PagesCount)
                    ));

                break;
            }

            doc.Save(BookDal.fileName);
        }

        public static void DeleteCategory(int Id)
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


        public static void DeleteBook(int Id)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach (XElement category in doc.Root.Elements())
                foreach (XElement book in category.Elements("Books").Elements("Book"))
                    if (book.Attribute("ID").Value == Id.ToString())
                    {
                        book.Remove();
                        break;
                    }

            doc.Save(BookDal.fileName);
        }

        public static void UpdateCategory(int Id, BookCategory cat)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach (XElement category in doc.Root.Elements())
                if (category.Attribute("ID").Value == Id.ToString())
                {
                    category.SetElementValue("Name", cat.Name);
                    break;
                }

            doc.Save(BookDal.fileName);
        }

        public static void UpdateBook(int Id, Book book)
        {
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach (XElement category in doc.Root.Elements())
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

        public static List<Book> GetBooks(int catId)
        {
            List<Book> result = new List<Book>();
            XDocument doc = XDocument.Load(BookDal.fileName);

            foreach (XElement category in doc.Root.Elements())
            {
                if (Int32.Parse(category.Attribute("ID").Value) != catId)
                    continue;
                foreach (XElement bookElem in category.Elements("Books").Elements("Book"))
                {
                    Book book = new Book();
                    
                    book.ID = Int32.Parse(bookElem.Attribute("ID").Value);
                    book.Title = bookElem.Element("Title").Value;
                    book.Author = bookElem.Element("Author").Value;
                    book.PagesCount = Int32.Parse(bookElem.Element("Pages").Value);
                    book.Publication = bookElem.Element("Publication").Value;
                    book.Year = Int32.Parse(bookElem.Element("Year").Value);

                    result.Add(book);
                }
            }
            return result;
        }
    }
}
