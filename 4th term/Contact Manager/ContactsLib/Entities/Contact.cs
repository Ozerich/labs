using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace ContactsLib.Entities
{
    public class Contact : BaseEntity, IComparable<Contact>
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ContactGroup Group { get; set; }
        public List<ContactDetail> Details = new List<ContactDetail>();

        public Contact()
        {
            Id = -1;
            Group = new ContactGroup();
        }

        public Contact(int Id)
        {
            this.Id = Id;
            Select();
        }


        public int CompareTo(Contact other)
        {
            return Name.CompareTo(other.Name);
        }

        public override void Select()
        {
            SqlDataReader reader = new SqlCommand("SELECT * FROM Contact WHERE Id = " + Id, connection).ExecuteReader();
            reader.Read();
            
            Name = (string)reader["Name"];
            if (reader["Group_ID"] == "0")
                Group = new ContactGroup();
            else
                Group = new ContactGroup((int)reader["Group_ID"]);
        
            reader.Close();

            reader = new SqlCommand("SELECT * FROM ContactDetail WHERE Contact_ID = " + Id, connection).ExecuteReader();
            while (reader.Read())
            {
                ContactDetail cd = new ContactDetail();
                cd.Id = (int)reader["ID"];
                cd.Name = (string)reader["Name"];
                cd.Value = (string)reader["Value"];
                cd.Contact = this;

                Details.Add(cd);
            }
            reader.Close();
        }

        public override void Delete()
        {
            new SqlCommand("DELETE FROM ContactDetail WHERE Contact_ID = " + Id, connection).ExecuteScalar();
            new SqlCommand("DELETE FROM Contact WHERE ID = " + Id, connection).ExecuteScalar();
        }

        public override void Persist()
        {
            if (Id == -1)
            {
                SqlCommand com = new SqlCommand(
                       String.Format("INSERT INTO Contact (Name, Group_Id) OUTPUT INSERTED.id VALUES('{0}','{1}')", Name, Group.Id == -1 ? 0 : Group.Id), connection);
                Id = (int)com.ExecuteScalar();
            }
            else
                new SqlCommand(String.Format("UPDATE Contact SET Name='{0}', Group_ID='{1}' WHERE ID = {2}", Name, Group.Id, Id), connection).ExecuteScalar();
        }

        public static List<Contact> SelectAll()
        {
            List<Contact> result = new List<Contact>();

            SqlConnection connection = new Contact().connection;

            SqlDataReader reader = new SqlCommand("SELECT * FROM Contact", connection).ExecuteReader();
            while (reader.Read())
                result.Add(new Contact((int)reader["ID"]));
            reader.Close();

            return result;
        }
    }
}
