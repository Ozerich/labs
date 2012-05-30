using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Entities
{
    public class User : IEntity
    {

        public User(int id)
        {
            Id = id;
            Select();
        }

        public User()
        {
            Id = -1;
        }

        public User(string login, string password) : this()
        {
            Login = login;
            Password = password;
        }
        
        public int Id { get; private set; }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }




        public void Persist()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            if (Id == -1)
                Id = (int)(new SqlCommand(String.Format("INSERT INTO Users (Login, Password) OUTPUT INSERTED.id VALUES ('{0}','{1}')", Login, Password)).ExecuteScalar());
            else
                new SqlCommand(
                    String.Format("UPDATE Users SET Login = '{0}', Password = '{1}' WHERE id = {2}", Login, Password, Id)).ExecuteNonQuery();

            connection.Close();
        }

        public void Delete()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            new SqlCommand(String.Format("DELETE FROM Users WHERE Id = {0}", Id)).ExecuteNonQuery();

            connection.Close();
        }


        public void Select()
        {
            throw new NotImplementedException();
        }

        public static User FindByLogin(string login)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.CommandText = "SELECT ID, Password FROM users WHERE [login] = @login";

            

            var reader = new SqlCommand(String.Format("SELECT * FROM users WHERE login = '{0}'", login)).ExecuteReader();
            if (reader.HasRows == false)
                return null;

            reader.Read();
            
            int id = (int)reader["Id"];

            reader.Close();
            connection.Close();

            return new User(id);
        }
    }
}
