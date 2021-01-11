using Mythical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.RenderModels
{
    public class ServiceRender
    {
        public string Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public ServiceStatus ServiceStatus { get; set; }
        public User CreatedBy { get; set; }
        public List<UserGroupRender> InvolvedUsers { get; set; }
    }
}
