
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
    }
}
