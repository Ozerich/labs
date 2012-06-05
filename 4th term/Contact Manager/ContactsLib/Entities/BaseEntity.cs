using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace ContactsLib.Entities
{
    public abstract class BaseEntity
    {
        protected SqlConnection connection;

        public BaseEntity()
        {
            try
            {
                connection = new SqlConnection("Initial Catalog=MSDB2; Integrated Security=SSPI");
                connection.Open();
            }
            catch (Exception ex)
            {
                int a = 0;    
            }
        }

        public abstract void Persist();
        public abstract void Delete();
        public abstract void Select();
    }
}
