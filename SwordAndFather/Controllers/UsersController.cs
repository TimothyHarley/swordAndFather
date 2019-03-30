using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Data;
using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // square bracketed things are called Attributes.  
    // They're not there for us; they're there as metadata for the interwebs
    public class UsersController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;

        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }

        [HttpPost("register")]
        public ActionResult<int> AddUser(CreateUserRequest createRequest)
        {

            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a username and password" });
            }

            var userRepository = new UserRepository();
            var newUser = _userRepository.AddUser(createRequest.Username, createRequest.Password);

            //http response
            return Created($"api/users/{newUser.Id}", newUser);
        }

    }
}