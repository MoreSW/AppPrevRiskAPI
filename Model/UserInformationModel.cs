﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using appPrevencionRiesgos.Data.Entities;

namespace appPrevencionRiesgos.Model.Security
{
    public class UserInformationModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public IList<IDictionary<string, string>>? ConfidenceUsers { get; set; }
    }
}
