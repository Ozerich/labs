using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Entities;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;


namespace DAL
{
    public class UserDal
    {

        public static void CreateUser(string login, string password)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();      
                SqlCommand com = connection.CreateCommand();
                com.Parameters.Add("@login", SqlDbType.NVarChar);
                com.Parameters["@login"].Value = login;
                com.Parameters.Add("@pass", SqlDbType.NVarChar);
                com.Parameters["@pass"].Value = password;
                com.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                com.Parameters["@id"].Value = Guid.NewGuid();
                com.CommandText = @"INSERT INTO [BookCatalog].[dbo].[users]
                              ([login], [password])
                              VALUES
                              (@login, @pass);";
                com.ExecuteNonQuery();
            
            connection.Close();
        }

        public static User FindByLogin(string login)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();

            com.Parameters.Add("@login", SqlDbType.NVarChar);
            com.Parameters["@login"].Value = login;

            com.CommandText = "SELECT ID, Password FROM users WHERE [login] = @login";

            var reader = com.ExecuteReader();
            if (reader.HasRows == false)
                return null;

            int passwordId = reader.GetOrdinal("password");
            int idId = reader.GetOrdinal("id");

            string password = "";
            int id = 0;

            while (reader.Read())
            {
                password = (string)reader[passwordId];
                id = (int)reader[idId];
            }

            reader.Close();
            connection.Close();

            return new User(login, password, id);
        }

        public static void DeleteUser(User user)
        {
            var connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            SqlCommand com = connection.CreateCommand();
            com.Parameters.Add("@id", SqlDbType.Int);
            com.Parameters["@id"].Value = user.Id;

            com.CommandText = "DELETE FROM users WHERE [ID] = @id";
            com.ExecuteNonQuery();

            connection.Close();
        }

    }
}
