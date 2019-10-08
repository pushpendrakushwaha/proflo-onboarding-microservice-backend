using Onboarding_Backend.Onboarding_BussinessLayer.Exception;
using Onboarding_Backend.Onboarding_DataAccess;
using Onboarding_Backend.Onboarding_Entities;

namespace Onboarding_Backend.Onboarding_BussinessLayer
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        public UserServices(IUserRepository repository)
        {
            userRepository = repository;
        }
       

        public void RegisterUser(Entities entities)
        {
            var result = userRepository.GetUSerByEmailId(entities.Email);

            if (result == null)
            {
                userRepository.RegisterUser(entities);
            }
            else
            {
                throw new UserAlreadyExisted("The user is already Exists in database");
            }

        }

        public bool UpdateUser(string Uid, Entities entities)
        {
            var result = userRepository.GetUSerByEmailId(entities.Email);
            if (result == null)
            {
                throw new UserNotFoundException("User Not found");
            }
            else
            {
                result.FirstName = entities.FirstName;
                result.LastName = entities.LastName;
                result.avatar = entities.avatar;
                result.Bio = entities.Bio;
                result.UserName = entities.UserName;
                result.Password = entities.Password;

                return userRepository.UpdateUser(Uid, entities);

            }
        }

        public bool DeleteUser(string Uid)
        {
            var result = userRepository.GetUserByUserId(Uid);
            if (result == null)
            {
                throw new UserNotFoundException("Notes Not found");
            }
            else
            {
                return userRepository.DeleteUser(Uid);
            }
        }

        public Entities GetUSerByEmailId(string email)
        {
            var result = userRepository.GetUSerByEmailId(email);
            if (result == null)
            {
                throw new UserNotFoundException("User Not found");
            }
            else
            {
                return result;
            }
        }

        public Entities GetUserByUserId(string Uid)
        {
            var result = userRepository.GetUserByUserId(Uid);
            if (result == null)
            {
                throw new UserNotFoundException("User Not found");
            }
            else
            {
                return result;
            }
        }

        public Entities UserLogin(string email, string password)
        {
            var user = userRepository.UserLogin(email, password);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new UserNotFoundException("User Not Found");
            }

        }
    }
}
