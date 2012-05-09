using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;


namespace BLL
{
    public static class Users
    {

        public static void CreateUser(string login, string password)
        {
            UserDal.CreateUser(login, password);
        }

        public static bool CheckExist(string login)
        {
            User user = UserDal.FindByLogin(login);
            return user != null;
        }

        public static User Auth(string login, string password)
        {
            User user = UserDal.FindByLogin(login);
            return user != null && user.Password == password ? user : null;
        }
    }
}
