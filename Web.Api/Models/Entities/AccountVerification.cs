using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Web.Api.Models.Entities
{
    public class AccountVerification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }
        public string AuthCode { get; set; }
        public int Attempts { get; set; }
        public DateTime Expiry { get; set; }
    }
}
