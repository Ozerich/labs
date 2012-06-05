using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace ContactsLib.Entities
{
    public class ContactGroup : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactGroup()
        {
            Id = 0;
        }

        public ContactGroup(int Id)
        {
            this.Id = Id;
            Select();
        }

        public override void Delete()
        {
            new SqlCommand("DELETE FROM Contact WHERE Group_ID = " + Id, connection).ExecuteScalar();
            new SqlCommand("DELETE FROM ContactGroup WHERE ID = " + Id, connection).ExecuteScalar();
        }

        public override void Persist()
        {
            if (Id == 0)
                Id = (int)new SqlCommand(String.Format("INSERT INTO ContactGroup (Name) OUTPUT INSERTED.id VALUES('{0}')", Name), connection).ExecuteScalar();
            else
                new SqlCommand(String.Format("UPDATE ContactGroup Name = '{0}' WHERE ID = {1}", Name, Id), connection).ExecuteScalar();
        }

        public override void Select()
        {
            SqlDataReader reader = new SqlCommand("SELECT * FROM ContactGroup WHERE ID = " + Id, connection).ExecuteReader();
            reader.Read();
            if (reader.HasRows == false)
                return;
            Name = (string)reader["Name"];
        }

        public override string ToString()
        {
            return Name;    
        }

        public ContactGroup GetGroupByName(string name)
        {
            ContactGroup result = null;
            SqlDataReader reader = new SqlCommand(String.Format("SELECT * FROM ContactGroup WHERE Name = '{0}'", name), connection).ExecuteReader();
            reader.Read();
            if (reader.HasRows)
                result = new ContactGroup((int)reader["ID"]);
            else
            {
                result = new ContactGroup();
                result.Name = name;
                result.Persist();
            }

            reader.Close();
            return result;
        }

    }
}
