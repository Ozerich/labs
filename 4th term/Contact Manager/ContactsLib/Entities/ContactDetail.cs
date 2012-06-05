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
    public class ContactDetail : BaseEntity
    {
        public int Id { get; set; }
        public Contact Contact { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public ContactDetail()
        {
            Id = -1;
        }

        public ContactDetail(int id)
        {
            Id = id;
            Select();
        }

        public override void Persist()
        {
            if (Id == -1)
                Id = (int)new SqlCommand(String.Format("INSERT INTO ContactDetail (Contact_ID, Name, Value) OUTPUT INSERTED.id VALUES ('{0}', '{1}', '{2}')",
                    Contact.Id, Name, Value), connection).ExecuteScalar();
            else
                new SqlCommand(String.Format("UPDATE ContactDetail SET Contact_ID = '{0}', Name = '{1}', Value = '{2}' WHERE ID = {3}", Contact.Id, Name, Value, Id), 
                    connection).ExecuteScalar();
        }

        public override void Delete()
        {
            new SqlCommand("DELETE FROM ContactDetail WHERE Id = " + Id, connection).ExecuteScalar();        
        }

        public override void Select()
        {
            SqlDataReader reader = new SqlCommand("SELECT * FROM ContactDetail WHERE Id = " + Id, connection).ExecuteReader();
            reader.Read();

            Name = (string)reader["Name"];
            Value = (string)reader["Value"];
            Contact = new Contact((int)reader["Contact_ID"]);
        }
    }
}
