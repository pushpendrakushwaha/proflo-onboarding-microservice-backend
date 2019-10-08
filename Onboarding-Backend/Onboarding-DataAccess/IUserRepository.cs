using Onboarding_Backend.Onboarding_Entities;


namespace Onboarding_Backend.Onboarding_DataAccess
{
    public interface IUserRepository
    {
        void RegisterUser(Entities entities);
        bool UpdateUser(string Uid, Entities entities);
        bool DeleteUser(string Uid);

        Entities GetUSerByEmailId(string email);
        Entities GetUserByUserId(string Uid);

        Entities UserLogin(string email, string password);
    }
}
