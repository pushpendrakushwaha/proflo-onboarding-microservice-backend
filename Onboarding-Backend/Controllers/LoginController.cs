using Microsoft.AspNetCore.Mvc;
using Onboarding_Backend.Onboarding_BussinessLayer;
using Onboarding_Backend.Onboarding_BussinessLayer.Exception;
using Onboarding_Backend.Onboarding_Entities;
using Onboarding_Backend.Services;
using System;
using System.Security.Claims;

namespace Onboarding_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IEmailNotificationService emailNotificationService;
        public LoginController(IUserServices Services, IEmailNotificationService emailNotificationService)
        {
            userServices = Services;
            this.emailNotificationService = emailNotificationService;
        }

        //api/login
        [HttpPost]

        public IActionResult UserLogin([FromBody] Entities entities)
        {
            try
            {
                string email = entities.Email;
                string password = entities.Password;
                Entities _entities = userServices.UserLogin(email, password);
                entities.Password = null;
                var myToken = emailNotificationService.GenerateToken(entities);
                return Ok( new { userAccessToken = myToken });
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch
            {
                return StatusCode(500, "Some server error");

            }
            // string email = entities.Email;
            // string password = entities.Password;
            //// var value = (Request.HttpContext.User.Identity as ClaimsIdentity).FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            // foreach (var claim in (Request.HttpContext.User.Identity as ClaimsIdentity).Claims)
            // {
            //     Console.WriteLine(claim.Value);
            // }

            // //var testvalue = value;
            // try
            // {
            //     string myToken = emailNotificationService.GenerateToken(entities);
            //     userServices.UserLogin(email, password);
            //     return Ok(myToken);
            // }
            // catch (Exception ex)
            // {
            //     return NotFound(ex.Message);
            // }
            //// return Ok(true);
        }

        private object GenerateToken(Entities entities1, Entities entities2)
        {
            throw new NotImplementedException();
        }
    }

}