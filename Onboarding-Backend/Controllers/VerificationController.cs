using Microsoft.AspNetCore.Mvc;
using Onboarding_Backend.Onboarding_BussinessLayer;
using Onboarding_Backend.Onboarding_Entities;
using Onboarding_Backend.Services;

namespace Onboarding_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private IEmailNotificationService _emailNS;
        private readonly IUserServices userServices;
        public VerificationController(IEmailNotificationService emailNS, IUserServices services)
        {
            userServices = services;
            _emailNS = emailNS;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery] string token)
        {
            Entities entities = _emailNS.VerifyAndDecodeEntities(token);
            var redirectUrl = "http://onboarding.proflo.cgi-wave7.stackroute.io/signup";
            
            return Redirect(redirectUrl);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Entities entities)
        {
            _emailNS.SendEmail(entities);
            return Ok("mail send");
        }
    }
}
