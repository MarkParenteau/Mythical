using MongoDB.Driver;
using Mythical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Services
{
    public class ServiceTypeService
    {
        private readonly IMongoCollection<ServiceType> _serviceTypes;

        public ServiceTypeService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _serviceTypes = database.GetCollection<ServiceType>(settings.ServiceTypeCollectionName);
        }

        public List<ServiceType> Get()
        {
            return _serviceTypes.Find(ServiceType => true).ToList();
        }

        public ServiceType Get(string id)
        {
            try
            {
                return _serviceTypes.Find<ServiceType>(ServiceType => ServiceType.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ServiceType Create(ServiceType ServiceType)
        {
            _serviceTypes.InsertOne(ServiceType);
            return ServiceType;
        }

        public void Update(string id, ServiceType ServiceTypeIn)
        {
            _serviceTypes.ReplaceOne(ServiceType => ServiceType.Id == id, ServiceTypeIn);
        }

        public void Remove(ServiceType ServiceTypeIn)
        {
            _serviceTypes.DeleteOne(ServiceType => ServiceType.Id == ServiceTypeIn.Id);
        }

        public void Remove(string id)
        {
            _serviceTypes.DeleteOne(ServiceType => ServiceType.Id == id);
        }
    }
}
