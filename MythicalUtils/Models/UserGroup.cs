using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Mythical.Models
{
    public class UserGroup
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoleId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public IList<string> Users { get; set; }
    }
}