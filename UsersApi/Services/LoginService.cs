﻿using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.Data.Requests;

namespace UsersApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        internal Result Login(LoginRequest request)
        {
            Task<SignInResult> resultIndentity = _signInManager
                .PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (resultIndentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                    user.NormalizedUserName == request.UserName.ToUpper());

                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login falhou");
        }
    }
}
