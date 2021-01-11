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
    public class ServicesController : ControllerBase
    {
        private readonly ServiceService _serviceService;

        public ServicesController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Returns a list of all services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Service>> Get()
        {
            return _serviceService.Get();
        }

        /// <summary>
        /// Returns the service with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetService")]
        public ActionResult<Service> Get(string id)
        {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound("Service not found");
            }

            return service;
        }

        /// <summary>
        /// Creates and return a service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Service> Post(Service service)
        {
            _serviceService.Create(service);
            return CreatedAtRoute("GetService", new { id = service.Id.ToString() }, service);
        }

        // PUT: api/Service/5
        [HttpPut("{id}")]
        public ActionResult<Service> Update(string id, Service updatedService)
        {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound("Service not found");
            }

            _serviceService.Update(id, updatedService);

            return NoContent();
        }

        /// <summary>
        /// Deletes the service with specified id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<Service> Delete(string id)
        {
            var service = _serviceService.Get(id);

            if (service == null)
            {
                return NotFound("Service not found");
            }

            _serviceService.Remove(service.Id);

            return NoContent();
        }
    }
}
