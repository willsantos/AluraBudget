using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public UserManager<IdentityUser<int>> UserManager { get; }

        public Result AddUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity,createDto.Password);
            if (resultIdentity.Result.Succeeded)
            {
                var ActivationCode = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                _emailService.SendEmail(new[] {userIdentity.Email},"Link de Ativação",userIdentity.Id,ActivationCode);
                return Result.Ok().WithSuccess(ActivationCode);
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
