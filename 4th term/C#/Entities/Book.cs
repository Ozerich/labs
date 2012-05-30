using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Entities
{
    public class Book : IEntity
    {
        public int Id { get; private set; }

        public Book()
        {
            Id = -1;
        }

        public Book(int id)
        {
            Id = id;
            Select();
        }

        private int year;
        private int pagesCount;

        public int parentId { get; set; }

        public List<string> Tags { get; set; } 

        public string Title
        {
            get;
            set;
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (value > DateTime.Now.Year + 1 || value <= 1000)
                    throw new ArgumentException("Year is incorrect");
                this.year = value;
            }
        }

        public int PagesCount
        {
            get
            {
                return pagesCount;
            }
            set
            {
                if (value < 0 || value > 10000)
                    throw new ArgumentException("Pages count is incorrect");
                this.pagesCount = value;
            }
        }

        public EnumFileFormat FileFormat
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Publication
        {
            get;
            set;
        }


        public string Genre
        {
            get;
            set;
        }



        public void Persist()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            if (Id == -1)
                Id = (int)(new SqlCommand(String.Format("INSERT INTO Books (Category_ID, Title, Author, Publication, Year, Pages, FileFormat, Genre) OUTPUT INSERTED.id VALUES ('{7}','{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    Title, Author, Publication, Year, PagesCount, FileFormat, Genre, parentId), connection).ExecuteScalar());
            else
                new SqlCommand(
                    String.Format("UPDATE Books SET Category_ID = '{8}', Title = '{0}', Author = '{1}', Publication = '{2}', Year = '{3}', Pages = '{4}', FileFormat = '{5}', Genre = '{6}' WHERE id = {7}",
                        Title, Author, Publication, Year, PagesCount, FileFormat, Genre, Id, parentId), connection).ExecuteNonQuery();

            new SqlCommand(String.Format("DELETE FROM Tags WHERE Book_Id = {0}", Id),connection).ExecuteNonQuery();

            foreach(string tag in Tags)
                new SqlCommand(String.Format("INSERT INTO Tags (Book_Id, Tag) VALUES ('{0}', '{1}')", Id, tag), connection).ExecuteNonQuery();

            connection.Close();
        }

        public void Delete()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            new SqlCommand(String.Format("DELETE FROM Tags WHERE Book_Id = {0}", Id), connection).ExecuteNonQuery();

            new SqlCommand(String.Format("DELETE FROM Books WHERE Id = {0}", Id), connection).ExecuteNonQuery();

            connection.Close();
        }

        public static List<Book> SelectAll(int catId = 0)
        {
            List<Book> result = new List<Book>();

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlDataReader reader = new SqlCommand("SELECT * FROM Books " + (catId == 0 ? "" : "WHERE Category_ID = " + catId), connection).ExecuteReader();
            while (reader.Read())
            {
                Book book = new Book(Int32.Parse(reader["ID"].ToString()));
                result.Add(book);
            }

            reader.Close();

            connection.Close();

            return result;
        }


        public void Select()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlDataReader reader = new SqlCommand("SELECT * FROM Books WHERE Id = " + Id, connection).ExecuteReader();
            reader.Read();

            Id = (int)reader["ID"];
            parentId = (int)reader["Category_ID"];
            Title = (string)reader["Title"];
            Author = (string)reader["Author"];
            Year = (int)reader["Year"];
            PagesCount = (int)reader["Pages"];
            Publication = (string)reader["Publication"];
            Genre = (string)reader["Genre"];

            Tags = new List<string>();
           
            reader.Close();

            SqlDataReader reader2 = new SqlCommand("SELECT * FROM Tags WHERE Book_Id = " + Id, connection).ExecuteReader();
            
            while (reader2.Read())
                Tags.Add(reader2["Tag"].ToString());

            connection.Close();
        }
    }
}
