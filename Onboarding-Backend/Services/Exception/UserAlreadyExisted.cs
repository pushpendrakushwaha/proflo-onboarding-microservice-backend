using System;

namespace Onboarding_Backend.Onboarding_BussinessLayer.Exception
{
    public class UserAlreadyExisted: ApplicationException
    {
        public UserAlreadyExisted() { }
        public UserAlreadyExisted(string message) : base(message) { }

    }
}
