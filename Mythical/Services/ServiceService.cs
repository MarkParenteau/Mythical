using MongoDB.Driver;
using Mythical.Models;
using Mythical.RenderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Services
{
    public class ServiceService
    {
        private readonly IMongoCollection<Service> _services;

        public ServiceService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _services = database.GetCollection<Service>(settings.ServiceCollectionName);
        }

        public List<Service> Get()
        {
            return _services.Find(Service => true).ToList();
        }

        public Service Get(string id)
        {
            try
            {
                return _services.Find<Service>(Service => Service.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Service Create(Service Service)
        {
            _services.InsertOne(Service);
            return Service;
        }

        public void Update(string id, Service ServiceIn)
        {
            _services.ReplaceOne(Service => Service.Id == id, ServiceIn);
        }

        public void Remove(Service ServiceIn)
        {
            _services.DeleteOne(Service => Service.Id == ServiceIn.Id);
        }

        public void Remove(string id)
        {
            _services.DeleteOne(Service => Service.Id == id);
        }
    }
}
