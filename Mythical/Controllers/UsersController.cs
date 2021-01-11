using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mythical.Models;
using Mythical.Services;

namespace Mythical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        /// <summary>
        /// Returns the user with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return user;
        }

        /// <summary>
        /// Creates and return a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            _userService.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public ActionResult<User> Update(string id, User updatedUser)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _userService.Update(id, updatedUser);

            return NoContent();
        }

        /// <summary>
        /// Deletes the user with specified id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _userService.Remove(user.Id);

            return NoContent();
        }
    }
}
