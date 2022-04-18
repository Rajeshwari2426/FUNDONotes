using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Services;

namespace FunDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly FunDoContext _config;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {         
            var result=userBL.Register(userRegistration);
            if(result!=null)
                return Ok(new {success=true,message="Register Successful",data=result});
            else
                return BadRequest(new { success = false, message = "Register UnSuccessful"});
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var result = userBL.Login(userLogin);
            if (result != null)
                return Ok(new { success = true, message = "Login Successful", data = result });
            else
                return BadRequest(new { success = false, message = "Login UnSuccessful" });

        }
        //[HttpGet]
        //public string GetRandomToken()
        //{
        //    var jwt = new UserRl(_config);
        //    var token = jwt.GenerateSecurityToken("fake@email.com",1);
        //    return token;
        //}
    }
}
