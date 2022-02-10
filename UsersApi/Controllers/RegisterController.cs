using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsersApi.Data.DTO;
using UsersApi.Data.Requests;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("/cadastro")]
    public class RegisterController: ControllerBase
    {
        private RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult UserRegister(CreateUserDto createDto)
        {
            Result result = _registerService.AddUser(createDto);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes.FirstOrDefault());
        }

        [HttpPost("/active")]
        public IActionResult ActiveUserAccount(ActiveAccountRequest request)
        {
            Result result = _registerService.ActiveAccount(request);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

    }
}
