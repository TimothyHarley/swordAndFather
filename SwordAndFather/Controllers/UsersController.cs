using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // square bracketed things are called Attributes.  
    // They're not there for us; they're there as metadata for the interwebs
    public class UsersController : ControllerBase
    {
        static List<User> _users = new List<User>();

        [HttpPost("register")]
        public int AddUser(string username, string password)
        {
            var newUser = new User(username, password);

            newUser.Id = _users.Count + 1;

            _users.Add(newUser);

            return newUser.Id;
        }

    }
}