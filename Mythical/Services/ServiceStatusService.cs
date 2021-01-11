using MongoDB.Driver;
using Mythical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Services
{
    public class ServiceStatusService
    {
        private readonly IMongoCollection<ServiceStatus> _serviceStatuses;

        public ServiceStatusService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _serviceStatuses = database.GetCollection<ServiceStatus>(settings.ServiceStatusCollectionName);
        }

        public List<ServiceStatus> Get()
        {
            return _serviceStatuses.Find(ServiceStatus => true).ToList();
        }

        public ServiceStatus Get(string id)
        {
            try
            {
                return _serviceStatuses.Find<ServiceStatus>(ServiceStatus => ServiceStatus.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ServiceStatus Create(ServiceStatus ServiceStatus)
        {
            _serviceStatuses.InsertOne(ServiceStatus);
            return ServiceStatus;
        }

        public void Update(string id, ServiceStatus ServiceStatusIn)
        {
            _serviceStatuses.ReplaceOne(ServiceStatus => ServiceStatus.Id == id, ServiceStatusIn);
        }

        public void Remove(ServiceStatus ServiceStatusIn)
        {
            _serviceStatuses.DeleteOne(ServiceStatus => ServiceStatus.Id == ServiceStatusIn.Id);
        }

        public void Remove(string id)
        {
            _serviceStatuses.DeleteOne(ServiceStatus => ServiceStatus.Id == id);
        }
    }
}
