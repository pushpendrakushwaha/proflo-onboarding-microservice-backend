using Onboarding_Backend.Onboarding_Entities;

namespace Onboarding_Backend.Services
{
    public interface IEmailNotificationService
    {
        string GenerateToken(Entities entities);

        void SendEmail(Entities entities);

        Entities VerifyAndDecodeEntities(string token);

    }
}
