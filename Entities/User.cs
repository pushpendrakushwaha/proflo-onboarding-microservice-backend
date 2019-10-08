using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    class User
    {
        [BsonId]
        public int Uid { get; set; }
        [BsonElement("firstname")]
        public string FirstName { get; set; }
        [BsonElement("lastname")]
        public string LastName { get; set; }
        [BsonElement("username")]
        public string UserName { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("avatar")]
        public string avatar { get; set; }
        [BsonElement("bio")]
        public string Bio { get; set; }
        [BsonElement("creation date")]
        public DateTime GetDateTime { get; set; }
    }
}
