using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceSite.Models
{
    public class Login
    {
        public bool IsAuthenticate = false;
        public int userID;



        public bool IsAuth()
        {
            return IsAuthenticate;
        }

        public int user()
        {
            return userID;
        }
    }
}