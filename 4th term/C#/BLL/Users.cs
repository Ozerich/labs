using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;


namespace BLL
{
    public static class Users
    {

        public static void CreateUser(string login, string password)
        {
            User user = new User(login, password);
            user.Persist();
        }

        public static bool CheckExist(string login)
        {
            User user = User.FindByLogin(login);
            return user != null;
        }

        public static User Auth(string login, string password)
        {
            User user = User.FindByLogin(login);
            return user != null && user.Password == password ? user : null;
        }
    }
}
