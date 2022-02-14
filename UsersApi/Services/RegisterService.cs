using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsersApi.Data.DTO;
using UsersApi.Data.Requests;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public RegisterService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService
, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public UserManager<IdentityUser<int>> UserManager { get; }

        public Result AddUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            


            //var createRoleResult = _roleManager
            //    .CreateAsync(new IdentityRole<int>("admin")).Result;

            //var userRoleResult = _userManager
            //    .AddToRoleAsync(userIdentity, "admin").Result;






            if (resultIdentity.Result.Succeeded)
            {
                Task<IdentityResult> roleResult = _userManager.AddToRoleAsync(userIdentity, "regular");

                if (roleResult.Result.Succeeded)
                {
                    var ActivationCode = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                    var encodedCode = HttpUtility.UrlEncode(ActivationCode);

                    _emailService.SendEmail(new[] { userIdentity.Email }, "Link de Ativação", userIdentity.Id, encodedCode);
                    return Result.Ok().WithSuccess(ActivationCode);
                }

                
            }

            return Result.Fail("Falha ao cadastrar o usuário");
        }

        public Result ActiveAccount(ActiveAccountRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == request.UserId);

            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.ActivationCode).Result;

            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail("Falha ao ativar a conta");
        }
    }
}
