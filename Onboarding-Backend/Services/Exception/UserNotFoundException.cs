using System;

namespace Onboarding_Backend.Onboarding_BussinessLayer.Exception
{
    public class UserNotFoundException: ApplicationException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
    }
}

