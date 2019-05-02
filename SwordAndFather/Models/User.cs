using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class User
    {

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(){} //paramerterless constructor is needed for Dapper. . .I think?  
        // Can also go with -No constructors, or -constructor that passes all the things(in this case we are missing ID)
        // Probably go with no constructors for the future.

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}