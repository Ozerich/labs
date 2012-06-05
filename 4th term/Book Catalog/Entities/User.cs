using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Data.Linq.Mapping;

namespace Entities
{
    [Table(Name = "Users")]
    public class User
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; private set; }

        [Column]
        public string Login { get; set; }

        [Column]
        public string Password { get; set; }


        public static User FindByLogin(string login)
        {
            Catalog catalog = new Catalog();

            var q = from user in catalog.Users where user.Login == login select user;
            return q.First();
        }

        public bool IsAdmin
        {
            get
            {
                return Login == "admin";
            }
            private set { }
        }


        public bool HasBook(int book_Id)
        {
            Catalog catalog = new Catalog();

            var q = from book in catalog.UserBooks where book.Book_Id == book_Id where book.User_Id == Id select book;

            return q.Count() > 0;
        }


     }
}
