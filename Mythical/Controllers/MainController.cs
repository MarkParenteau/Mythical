using Microsoft.AspNetCore.Mvc;
using Mythical.Managers;
using Mythical.RenderModels;
using Mythical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private MainManager mainManager;

        public MainController(ServiceService serviceService, RoleService roleService, UserService userService, ServiceStatusService serviceStatusService, ServiceTypeService serviceTypeService)
        {
            mainManager = new MainManager(serviceService, roleService, userService, serviceStatusService, serviceTypeService);
        }

        /// <summary>
        /// Returns the service with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetServiceDetails")]
        public ActionResult<ServiceRender> Get(string id)
        {
            var service = mainManager.GetService(id);

            if (service == null)
            {
                return NotFound("Service not found");
            }

            return service;
        }

        /// <summary>
        /// Returns the services
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ServiceRender>> GetServices()
        {
            var service = mainManager.GetServices();

            if (service == null)
            {
                return NotFound("Service not found");
            }

            return service;
        }

    }
}
