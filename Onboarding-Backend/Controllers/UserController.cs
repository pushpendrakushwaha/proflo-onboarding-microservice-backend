using System;
using Microsoft.AspNetCore.Mvc;
using Onboarding_Backend.Onboarding_BussinessLayer;
using Onboarding_Backend.Onboarding_BussinessLayer.Exception;
using Onboarding_Backend.Onboarding_Entities;
using Onboarding_Backend.Services;

namespace Onboarding_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserServices userServices;
        private readonly IEmailNotificationService emailNotificationService;
        public UserController(IUserServices services, IEmailNotificationService emailNotificationService)
        {
            userServices = services;
            this.emailNotificationService = emailNotificationService;
        }

        // GET: api/user/
        [HttpGet("{id}")]
        public IActionResult Get(string Uid)
        {
            try
            {
                return Ok(userServices.GetUserByUserId(Uid));
            }
            catch (Exception)
            {
                throw new UserNotFoundException();

            }
        }

        // POST: api/user
        [HttpPost]
        public IActionResult Post([FromBody] Entities entities)
        {
            string authorizationToken = Request.Headers["Authorization"];
            Console.WriteLine(authorizationToken);
            var jwtToken = authorizationToken.Split(' ')[1];
            if (emailNotificationService.VerifyAndDecodeEntities(jwtToken) != null)
            {
                userServices.RegisterUser(entities);
                return Created("api/user", entities);
            }
            else 
            {
                // Access Forbidden
                return StatusCode(403, "Access Forbidden");
                
            }            
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public IActionResult Put(string Uid, [FromBody] Entities entities)
        {
            try
            {
                return Ok(userServices.UpdateUser(Uid,entities));
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string Uid)
        {
            try
            {
                return Ok(userServices.DeleteUser(Uid));
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
    }
}