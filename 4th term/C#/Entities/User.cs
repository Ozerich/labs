using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class User
    {
        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public bool IsAdmin { get; private set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
