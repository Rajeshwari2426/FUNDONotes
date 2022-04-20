using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Services;
using System.Security.Claims;

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
            var result = userBL.Register(userRegistration);
            if (result != null)
                return Ok(new { success = true, message = "Register Successful", data = result });
            else
                return BadRequest(new { success = false, message = "Register UnSuccessful" });
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
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            var result = userBL.ForgetPassword(email);
            if (result != null)
                return Ok(new { success = true, message = "Reset link sent", data = result });
            else
                return BadRequest(new { success = false, message = "reset link not sent" });

        }
        [Authorize]
        [HttpPost("ResetPasswordPassword")]
        public IActionResult Reset_Password(ResetPassword resetPassword)
        {
            var emailId = User.FindFirst(ClaimTypes.Email).Value.ToString();
            var result = userBL.Reset_Password(resetPassword, emailId);
            if (result != null)
                return Ok(new { success = true, message = "Password Reset success", data = result });
            else
                return BadRequest(new { success = false, message = "resetpassword not success" });

        }
    }
}
