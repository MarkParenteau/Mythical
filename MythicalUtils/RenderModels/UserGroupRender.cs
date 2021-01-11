using Mythical.Models;
using System.Collections.Generic;

namespace Mythical.RenderModels
{
    public class UserGroupRender
    {
        public Role Role { get; set; }
        public List<User> Users { get; set; }
    }
}