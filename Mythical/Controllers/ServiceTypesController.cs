using Microsoft.AspNetCore.Mvc;
using Mythical.Services;
using Mythical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly ServiceTypeService _serviceTypeService;

        public ServiceTypesController(ServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        /// <summary>
        /// Returns a list of all serviceType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ServiceType>> Get()
        {
            return _serviceTypeService.Get();
        }

        /// <summary>
        /// Returns the serviceType with the specified id if it exists, returns a 404 otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetServiceType")]
        public ActionResult<ServiceType> Get(string id)
        {
            var serviceType = _serviceTypeService.Get(id);

            if (serviceType == null)
            {
                return NotFound("ServiceType not found");
            }

            return serviceType;
        }

        /// <summary>
        /// Creates and return a serviceType
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ServiceType> Post(ServiceType serviceType)
        {
            _serviceTypeService.Create(serviceType);
            return CreatedAtRoute("GetServiceType", new { id = serviceType.Id.ToString() }, serviceType);
        }

        // PUT: api/ServiceType/5
        [HttpPut("{id}")]
        public ActionResult<ServiceType> Update(string id, ServiceType updatedServiceType)
        {
            var serviceType = _serviceTypeService.Get(id);

            if (serviceType == null)
            {
                return NotFound("ServiceType not found");
            }

            _serviceTypeService.Update(id, updatedServiceType);

            return NoContent();
        }

        /// <summary>
        /// Deletes the serviceType with specified id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<ServiceType> Delete(string id)
        {
            var serviceType = _serviceTypeService.Get(id);

            if (serviceType == null)
            {
                return NotFound("ServiceType not found");
            }

            _serviceTypeService.Remove(serviceType.Id);

            return NoContent();
        }
    }
}
