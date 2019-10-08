using Microsoft.AspNetCore.Mvc;
using Onboarding_Backend.Onboarding_Entities;
using Onboarding_Backend.Services;

namespace Onboarding_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IEmailNotificationService _emailNS;
        public TokenController(IEmailNotificationService emailNS)
        {
            _emailNS = emailNS;
        }

        // api/token
        [HttpPost]
        public IActionResult Post(Entities entities)
        {
            _emailNS.SendEmail(entities);
            return Ok();
        }
    }
}