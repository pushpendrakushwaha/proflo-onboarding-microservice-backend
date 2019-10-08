using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Onboarding_Backend.Onboarding_Entities;

namespace Onboarding_Backend.Onboarding_DataAccess
{
    public class UserDBContext
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;

        public UserDBContext() { }

        public UserDBContext(IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                mongoClient = new MongoClient(Environment.GetEnvironmentVariable("mongo_db"));
            }
            else
            {
                mongoClient = new MongoClient(configuration.GetSection("MongoDb:server").Value);
            }
            mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDB:database").Value);

           // mongoClient = new MongoClient(configuration.GetSection("MongoDB:server").Value);
           // mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDB:database").Value);
            // mongoClient = new MongoClient(Environment.GetEnvironmentVariable("mongo_db"));
            // mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("database"));
        }

        public IMongoCollection<Entities> UsersCollection => mongoDatabase.GetCollection<Entities>("Userscollection");
    }
}
