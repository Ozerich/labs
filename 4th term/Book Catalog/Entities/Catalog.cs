using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Entities
{
    public class Catalog : DataContext
    {
        public Table<BookCategory> BookCategories;
        public Table<Book> Books;
        public Table<BookTag> Tags;
        public Table<User> Users;
        public Table<UserBook> UserBooks;

        public Catalog() : base(Properties.Settings.Default.ConnectionString) { }
    }
}
