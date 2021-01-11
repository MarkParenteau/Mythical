using Mythical.Models;
using Mythical.RenderModels;
using Mythical.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mythical.Managers
{
    public class MainManager
    {
        private readonly ServiceService _serviceService;
        private readonly RoleService _roleService;
        private readonly UserService _userService;
        private readonly ServiceStatusService _serviceStatusService;
        private readonly ServiceTypeService _serviceTypeService;

        public MainManager(ServiceService serviceService, RoleService roleService, UserService userService, ServiceStatusService serviceStatusService, ServiceTypeService serviceTypeService)
        {
            _serviceService = serviceService;
            _roleService = roleService;
            _userService = userService;
            _serviceStatusService = serviceStatusService;
            _serviceTypeService = serviceTypeService;
        }

        public ServiceRender GetService(string id)
        {
            try
            {
                Service service = _serviceService.Get(id);
                if (service != null)
                {
                    ServiceRender serviceRender = new ServiceRender();
                    serviceRender.Id = service.Id;
                    serviceRender.ServiceStatus = _serviceStatusService.Get(service.ServiceStatusId);
                    serviceRender.ServiceType = _serviceTypeService.Get(service.ServiceTypeId);
                    serviceRender.CreatedBy = _userService.Get(service.CreatedBy);
                    serviceRender.InvolvedUsers = GetUserGroupRenders(service.InvolvedUsers);

                    return serviceRender;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ServiceRender> GetServices()
        {
            try
            {
                List<Service> services = _serviceService.Get();
                List<ServiceRender> serviceRenders = new List<ServiceRender>();
                foreach (var service in services)
                {
                    serviceRenders.Add(GetService(service.Id));
                }

                return serviceRenders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<UserGroupRender> GetUserGroupRenders(IList<UserGroup> involvedUsers)
        {
            List<UserGroupRender> render = new List<UserGroupRender>();
            List<string> userIds = new List<string>();
            List<string> roleIds = new List<string>();

            //Fetch required data to populate the render, this step exists to reduce the amount of calls to the db
            foreach (var userGroup in involvedUsers)
            {
                userIds.AddRange(userGroup.Users);
                roleIds.Add(userGroup.RoleId);
            }

            Dictionary<string, User> users = _userService.GetUsers(userIds).ToDictionary(u => u.Id, u => u);
            Dictionary<string, Role> roles = _roleService.GetRoles(roleIds).ToDictionary(r => r.Id, r => r);

            //Populate the render
            foreach (var userGroup in involvedUsers)
            {
                List<User> tempUsers = new List<User>();

                foreach (var user in userGroup.Users)
                    tempUsers.Add(users[user]);

                UserGroupRender userGroupRender = new UserGroupRender()
                {
                    Role = roles[userGroup.RoleId],
                    Users = tempUsers
                };

                render.Add(userGroupRender);
            }

            return render;
        }
    }
}