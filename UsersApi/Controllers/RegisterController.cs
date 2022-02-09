using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.DTO;

namespace UsersApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController: ControllerBase
    {
        [HttpPost]
        public IActionResult UserRegister(CreateUserDto createDto)
        {
            return Ok();
        }
    }
}
