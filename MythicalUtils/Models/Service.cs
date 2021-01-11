using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mythical.Models
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceTypeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceStatusId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; }
        public IList<UserGroup> InvolvedUsers { get; set; }
    }
}
