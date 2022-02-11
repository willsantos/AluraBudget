
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsersApi.Data.Requests;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult UserLogin(LoginRequest request)
        {
            Result result =_loginService.Login(request);
            if (result.IsFailed) return Unauthorized(result.Errors.FirstOrDefault().Message);
            return Ok(result.Successes.FirstOrDefault());
        }

        [HttpPost("/reset")]
        public IActionResult SendResetPassword(SendResetPasswordRequest request)
        {
            Result result = _loginService.SendLinkToResetPassword(request);
            if (result.IsFailed) return Unauthorized(result.Errors.FirstOrDefault());
            return Ok(result.Successes.FirstOrDefault());
        }

        [HttpPost("/change-password")]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            Result result = _loginService.ResetPassword(request);
            if (result.IsFailed) return Unauthorized(result.Errors.FirstOrDefault());
            return Ok(result.Successes.FirstOrDefault());
        }
    }
}
