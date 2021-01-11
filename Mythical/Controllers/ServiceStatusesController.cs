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
    public class ServiceStatusesController : ControllerBase
    {
        private readonly ServiceStatusService _serviceStatusService;

        public ServiceStatusesController(ServiceStatusService serviceStatusService)
        {
            _serviceStatusService = serviceStatusService;
        }

        /// <summary>
        /// Returns a list of all services
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ServiceStatus>> Get()
        {
            return _serviceStatusService.Get();
        }

        /// <summary>
        /// Returns the serviceStatus with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetServiceStatus")]
        public ActionResult<ServiceStatus> Get(string id)
        {
            var serviceStatus = _serviceStatusService.Get(id);

            if (serviceStatus == null)
            {
                return NotFound("ServiceStatus not found");
            }

            return serviceStatus;
        }

        /// <summary>
        /// Creates and return a serviceStatus
        /// </summary>
        /// <param name="serviceStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ServiceStatus> Post(ServiceStatus serviceStatus)
        {
            _serviceStatusService.Create(serviceStatus);
            return CreatedAtRoute("GetServiceStatus", new { id = serviceStatus.Id.ToString() }, serviceStatus);
        }

        // PUT: api/ServiceStatus/5
        [HttpPut("{id}")]
        public ActionResult<ServiceStatus> Update(string id, ServiceStatus updatedServiceStatus)
        {
            var serviceStatus = _serviceStatusService.Get(id);

            if (serviceStatus == null)
            {
                return NotFound("ServiceStatus not found");
            }

            _serviceStatusService.Update(id, updatedServiceStatus);

            return NoContent();
        }

        /// <summary>
        /// Deletes the serviceStatus with specified id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<ServiceStatus> Delete(string id)
        {
            var serviceStatus = _serviceStatusService.Get(id);

            if (serviceStatus == null)
            {
                return NotFound("ServiceStatus not found");
            }

            _serviceStatusService.Remove(serviceStatus.Id);

            return NoContent();
        }
    }
}
