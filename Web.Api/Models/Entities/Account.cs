using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Web.Api.Models.Entities
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
