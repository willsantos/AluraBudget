using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UsersApi.Data.DTO;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class RegisterService
    {
        private readonly IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
            
        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public UserManager<IdentityUser<int>> UserManager { get; }

        public Result AddUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);
            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity,createDto.Password);
            if (resultIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar o usuário");
        }
    }
}
