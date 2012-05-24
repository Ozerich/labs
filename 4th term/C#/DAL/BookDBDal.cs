﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

using Entities;

namespace DAL
{
    public class BookDal
    {

        public static void CreateCategory(string name)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();
            SqlCommand com = connection.CreateCommand();
            com.Parameters.Add("@Name", SqlDbType.NVarChar);
            com.Parameters["@Name"].Value = name;
            com.CommandText = @"INSERT INTO [BookCatalog].[dbo].[categories]
                              ([name])
                              VALUES
                              (@name);";
            com.ExecuteNonQuery();

            connection.Close();
        }

        public static void CreateBook(int parentId, Book book)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@Category_ID", SqlDbType.Int);
            com.Parameters.Add("@Title", SqlDbType.NVarChar);
            com.Parameters.Add("@Author", SqlDbType.NVarChar);
            com.Parameters.Add("@Pages", SqlDbType.Int);
            com.Parameters.Add("@Year", SqlDbType.Int);
            com.Parameters.Add("@FileFormat", SqlDbType.NVarChar);
            com.Parameters.Add("@Publication", SqlDbType.NVarChar);
            com.Parameters.Add("@Genre", SqlDbType.NVarChar);

            com.Parameters["@Category_ID"].Value = parentId;
            com.Parameters["@Title"].Value = book.Title;
            com.Parameters["@Author"].Value = book.Author;
            com.Parameters["@Pages"].Value = book.PagesCount;
            com.Parameters["@Year"].Value = book.Year;
            com.Parameters["@FileFormat"].Value = book.FileFormat;
            com.Parameters["@Publication"].Value = book.Publication;
            com.Parameters["@Genre"].Value = book.Genre;


            com.CommandText = @"INSERT INTO [BookCatalog].[dbo].[books] ([title], [category_id], [author], [pages], [year], [fileformat], [publication], [genre]) 
                VALUES (@Title, @Category_ID, @Author, @Pages, @Year, @FileFormat, @Publication, @Genre)";

            com.ExecuteNonQuery();

            com.Parameters.Clear();

            com.Parameters.Add("@Category_ID", SqlDbType.Int);
            com.Parameters.Add("@Title", SqlDbType.NVarChar);

            com.Parameters["@Category_ID"].Value = parentId;
            com.Parameters["@Title"].Value = book.Title;


            com.CommandText = "SELECT ID FROM [BookCatalog].[dbo].[books] WHERE [category_id] = @Category_ID AND [title] = @Title";

            int bookId = Int32.Parse(com.ExecuteScalar().ToString());

            com.Parameters.Clear();
            com.Parameters.Add("@Book_ID", SqlDbType.Int);
            com.Parameters.Add("@Text", SqlDbType.Text);

            com.Parameters["@Book_ID"].Value = bookId;

            foreach (string tag in book.Tags)
            {
                com.Parameters["@Text"].Value = tag;
                com.CommandText = "INSERT INTO [BookCatalog].[dbo].[tags] ([book_id], [tag]) VALUES (@Book_ID, @Text)";

                com.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static void DeleteCategory(int Id)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@ID", SqlDbType.Int);
            com.Parameters["@ID"].Value = Id;

            com.CommandText = "DELETE FROM [BookCatalog].[dbo].[categories] WHERE [id] = @ID";

            com.ExecuteNonQuery();
            connection.Close();
        }


        public static void DeleteBook(int Id)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@ID", SqlDbType.Int);
            com.Parameters["@ID"].Value = Id;

            com.CommandText = "DELETE FROM [BookCatalog].[dbo].[books] WHERE [id] = @ID";
            com.ExecuteNonQuery();

            com.CommandText = "DELETE FROM [BookCatalog].[dbo].[tags] WHERE [book_id] = @ID";
            com.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateCategory(int Id, BookCategory cat)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@ID", SqlDbType.Int);
            com.Parameters["@ID"].Value = Id;
            com.Parameters.Add("@Name", SqlDbType.NVarChar);
            com.Parameters["@Name"].Value = cat.Name;

            com.CommandText = "UPDATE [BookCatalog].[dbo].[categories] SET [name] = @Name WHERE [id] = @ID";

            com.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateBook(Book book)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@ID", SqlDbType.Int);
            com.Parameters["@ID"].Value = book.ID;

            com.Parameters.Add("@Title", SqlDbType.NVarChar);
            com.Parameters.Add("@Author", SqlDbType.NVarChar);
            com.Parameters.Add("@Pages", SqlDbType.Int);
            com.Parameters.Add("@Year", SqlDbType.Int);
            com.Parameters.Add("@FileFormat", SqlDbType.NVarChar);
            com.Parameters.Add("@Publication", SqlDbType.NVarChar);
            com.Parameters.Add("@Genre", SqlDbType.NVarChar);

            com.Parameters["@Title"].Value = book.Title;
            com.Parameters["@Author"].Value = book.Author;
            com.Parameters["@Pages"].Value = book.PagesCount;
            com.Parameters["@Year"].Value = book.Year;
            com.Parameters["@FileFormat"].Value = book.FileFormat;
            com.Parameters["@Publication"].Value = book.Publication;
            com.Parameters["@Genre"].Value = book.Genre;

            com.CommandText = @"UPDATE [BookCatalog].[dbo].[books] SET 
                        [Title] = @Title , [Author] = @Author , [Pages] = @Pages , [Year] = @Year , [FileFormat] = @FileFormat , [Publication] = @Publication, [Genre] = @Genre
                        WHERE [id] = @ID";

            com.ExecuteNonQuery();

            com.CommandText = "DELETE FROM [BookCatalog].[dbo].[tags] WHERE [book_id] = @Id";
            com.ExecuteNonQuery();

            com.Parameters.Add("@Text", SqlDbType.Text);

            foreach (string tag in book.Tags)
            {
                com.Parameters["@Text"].Value = tag;
                com.CommandText = "INSERT INTO [BookCatalog].[dbo].[tags] ([book_id], [tag]) VALUES (@Id, @Text)";

                com.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static List<BookCategory> GetCategories()
        {
            List<BookCategory> result = new List<BookCategory>();

            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();
            com.CommandText = "SELECT * FROM [BookCatalog].[dbo].[categories]";

            var reader = com.ExecuteReader();
            int NameId = reader.GetOrdinal("name");
            int idId = reader.GetOrdinal("id");

            while (reader.Read())
            {
                string name = (string)reader[NameId];
                int id = (int)reader[idId];

                result.Add(new BookCategory(id, name));
            }
            reader.Close();
            connection.Close();

            return result;
        }

        public static List<Book> GetBooks(int catId)
        {
            List<Book> result = new List<Book>();

            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@catId", SqlDbType.Int);
            com.Parameters["@catId"].Value = catId;

            com.CommandText = "SELECT * FROM [BookCatalog].[dbo].[books]" + (catId == 0 ? "" : "WHERE [category_id] = @catID");
            var reader = com.ExecuteReader();

            while (reader.Read())
            {
                Book book = new Book();

                book.ID = (int)reader["ID"];
                book.Title = (string)reader["Title"];
                book.Author = (string)reader["Author"];
                book.Year = (int)reader["Year"];
                book.PagesCount = (int)reader["Pages"];
                book.Publication = (string)reader["Publication"];

                string ff = (string)reader["FileFormat"];
                book.FileFormat = (EnumFileFormat)Enum.Parse(typeof(EnumFileFormat), ff);
                result.Add(book);
            }

            reader.Close();

            foreach (Book book in result)
            {

                SqlCommand comTag = connection.CreateCommand();

                comTag.Parameters.Add("@Id", SqlDbType.Int);
                comTag.Parameters["@Id"].Value = book.ID;


                comTag.CommandText = "SELECT [Tag] FROM [BookCatalog].[dbo].[Tags] WHERE [Book_Id] = @Id";


                var readerTag = comTag.ExecuteReader();

                book.Tags = new List<string>();

                while (readerTag.Read())
                    book.Tags.Add(readerTag["Tag"].ToString());


                readerTag.Close();
            }

            return result;
        }

        public static Book GetBook(int bookId)
        {

            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();
            com.Parameters.Add("@Id", SqlDbType.Int);
            com.Parameters["@Id"].Value = bookId;

            com.CommandText = "SELECT * FROM [BookCatalog].[dbo].[books] WHERE [id] = @Id";
            var reader = com.ExecuteReader();

            if (reader.HasRows == false)
                return null;

            Book book = new Book();

            reader.Read();

            book.ID = bookId;
            book.Title = (string)reader["Title"];
            book.Author = (string)reader["Author"];
            book.Year = (int)reader["Year"];
            book.PagesCount = (int)reader["Pages"];
            book.Publication = (string)reader["Publication"];
            book.Genre = (string)reader["Genre"];

            string ff = (string)reader["FileFormat"];
            book.FileFormat = (EnumFileFormat)Enum.Parse(typeof(EnumFileFormat), ff);

            reader.Close();


            com.Parameters.Clear();

            com.Parameters.Add("@Id", SqlDbType.Int);
            com.Parameters["@Id"].Value = bookId;

            com.CommandText = "SELECT [Tag] FROM [BookCatalog].[dbo].[Tags] WHERE [Book_Id] = @Id";
            reader = com.ExecuteReader();

            book.Tags = new List<string>();

            while (reader.Read())
                book.Tags.Add(reader["Tag"].ToString());

            return book;
        }
    }
}