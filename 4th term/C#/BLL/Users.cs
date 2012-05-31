using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;


namespace BLL
{
    public static class Users
    {
        public static User CurrentUser = null;

        public static void CreateUser(string login, string password)
        {
            User user = new User();
            user.Login = login;
            user.Password = password;

            Catalog catalog = new Catalog();

            catalog.Users.InsertOnSubmit(user);

            catalog.SubmitChanges();
        }

        public static bool CheckExist(string login)
        {
            User user = User.FindByLogin(login);
            return user != null;
        }

        public static User Auth(string login, string password)
        {
            User user = User.FindByLogin(login);
            
            bool success = user != null && user.Password == password;
            if (success)
            {
                CurrentUser = user;
            }

            return success ? user : null;
        }
    }
}
