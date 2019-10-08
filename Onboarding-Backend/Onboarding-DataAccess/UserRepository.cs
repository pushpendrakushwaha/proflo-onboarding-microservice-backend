using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
using Onboarding_Backend.Onboarding_Entities;

namespace Onboarding_Backend.Onboarding_DataAccess
{
    public class UserRepository : IUserRepository
    {
        string text = "hello world";
        private readonly UserDBContext userDBContext;
        public UserRepository(UserDBContext context)
        {
            userDBContext = context;
        }

        public void RegisterUser(Entities entities)
        {
            // entities.Uid = ObjectId.GenerateNewId();
            entities.Password = SHA1(SHA1(entities.Password + text ));

           userDBContext.UsersCollection.InsertOne(entities);
        }

        string SHA1(string input)
        {
            byte[] hash;
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            }
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
          
        }

        public Entities GetUSerByEmailId(string email)
        {
            return userDBContext.UsersCollection.Find(n => n.Email == email).FirstOrDefault();
        }


        public bool DeleteUser(string Uid)
        {
            var selectuser = userDBContext.UsersCollection.DeleteOne(n => n.Uid == Uid);
            return selectuser.IsAcknowledged && selectuser.DeletedCount > 0;
        }

      

        public bool UpdateUser(string Uid, Entities entities)
        {
            var filter = Builders<Entities>.Filter.Where(b => b.Uid == Uid);
            var Result = userDBContext.UsersCollection.ReplaceOne(filter, entities);
            return Result.IsAcknowledged && Result.ModifiedCount > 0;
        }

 
        public Entities GetUserByUserId(string Uid)
        {
            return userDBContext.UsersCollection.Find(n => n.Uid == Uid).FirstOrDefault();
        }

        public Entities UserLogin(string email, string password)
        {
            password = SHA1(SHA1(password + text));
            return userDBContext.UsersCollection.Find(u => u.Email == email && u.Password == password).FirstOrDefault();

        }


    }
}
