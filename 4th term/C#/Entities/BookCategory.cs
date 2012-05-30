using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Entities
{
 
    public class BookCategory : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public BookCategory()
        {
            Id = -1;
        }

        public BookCategory(string name)
            : this()
        {
            Name = name;
        }

        public BookCategory(int id)
        {
            Id = id;
            Select();
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<BookCategory> SelectAll()
        {
            List<BookCategory> result = new List<BookCategory>();

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();


            SqlDataReader reader = new SqlCommand("SELECT * FROM Categories", connection).ExecuteReader();

            while (reader.Read())
            {
                BookCategory bookCategory = new BookCategory((int)reader["ID"]);
                result.Add(bookCategory);
            }

            reader.Close();


            connection.Close();

            return result;
        }

        public void Persist()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            if (Id == -1)
                Id = (int)(new SqlCommand(String.Format("INSERT INTO Categories (Name) OUTPUT INSERTED.id VALUES ('{0}')", Name), connection).ExecuteScalar());
            else
                new SqlCommand(
                    String.Format("UPDATE Categories SET Name = '{0}' WHERE id = {1}", Name, Id), connection).ExecuteNonQuery();

            connection.Close();
        }

        public void Delete()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            new SqlCommand(String.Format("DELETE FROM Books WHERE Category_Id = {0}", Id), connection).ExecuteNonQuery();

            new SqlCommand(String.Format("DELETE FROM Categories WHERE id={0}", Id), connection).ExecuteNonQuery();

            connection.Close();
        }

        public void Select()
        {

            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlDataReader reader = new SqlCommand("SELECT * FROM Categories WHERE Id =" + Id, connection).ExecuteReader();
            reader.Read();

            Name = reader["Name"].ToString();

            reader.Close();
            connection.Close();
        }
    }
}
