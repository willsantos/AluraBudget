
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController: ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult UserLogout()
        {
            Result result = _logoutService.SignOut();
            if(result.IsFailed) return Unauthorized(result.Errors.FirstOrDefault().Message);
            return Ok(result.Successes);
        }
    }
}
