using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;


namespace BLL
{
    public static class Books
    {
        public static List<BookCategory> GetCategories()
        {
            return BookDal.GetCategories();
        }
    }
}
