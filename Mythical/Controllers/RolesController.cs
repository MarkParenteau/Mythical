using Microsoft.AspNetCore.Mvc;
using Mythical.Models;
using Mythical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Returns a list of all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Role>> Get()
        {
            return _roleService.Get();
        }

        /// <summary>
        /// Returns the role with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRole")]
        public ActionResult<Role> Get(string id)
        {
            var role = _roleService.Get(id);

            if (role == null)
            {
                return NotFound("Role not found");
            }

            return role;
        }

        /// <summary>
        /// Creates and return a role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Role> Post(Role role)
        {
            _roleService.Create(role);
            return CreatedAtRoute("GetRole", new { id = role.Id.ToString() }, role);
        }

        // PUT: api/role/5
        [HttpPut("{id}")]
        public ActionResult<Role> Update(string id, Role updatedrole)
        {
            var role = _roleService.Get(id);

            if (role == null)
            {
                return NotFound("Role not found");
            }

            _roleService.Update(id, updatedrole);

            return NoContent();
        }

        /// <summary>
        /// Deletes the role with specified id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<Role> Delete(string id)
        {
            var role = _roleService.Get(id);

            if (role == null)
            {
                return NotFound("Role not found");
            }

            _roleService.Remove(role.Id);

            return NoContent();
        }
    }
}
