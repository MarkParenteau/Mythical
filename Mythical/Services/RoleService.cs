using MongoDB.Driver;
using Mythical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Services
{
    public class RoleService
    {
        private readonly IMongoCollection<Role> _roles;

        public RoleService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _roles = database.GetCollection<Role>(settings.RoleCollectionName);
        }

        public List<Role> Get()
        {
            return _roles.Find(Role => true).ToList();
        }

        public Role Get(string id)
        {
            try
            {
                return _roles.Find<Role>(Role => Role.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Role> GetRoles(List<string> roleIds)
        {
            try
            {
                return _roles.Find<Role>(Role => roleIds.Contains(Role.Id)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Role Create(Role Role)
        {
            _roles.InsertOne(Role);
            return Role;
        }

        public void Update(string id, Role RoleIn)
        {
            _roles.ReplaceOne(Role => Role.Id == id, RoleIn);
        }

        public void Remove(Role RoleIn)
        {
            _roles.DeleteOne(Role => Role.Id == RoleIn.Id);
        }

        public void Remove(string id)
        {
            _roles.DeleteOne(Role => Role.Id == id);
        }
    }
}
