using Onboarding_Backend.Onboarding_Entities;

namespace Onboarding_Backend.Onboarding_BussinessLayer
{
    public interface IUserServices
    {
        void RegisterUser(Entities entities);

        bool DeleteUser(string Uid);

        bool UpdateUser(string Uid, Entities entities);

        Entities GetUSerByEmailId(string email);
        Entities GetUserByUserId(string Uid);

        Entities UserLogin(string email, string password);

    }
}
