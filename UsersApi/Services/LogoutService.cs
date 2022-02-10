using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UsersApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signinManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signinManager = signInManager;
        }

        public Result SignOut()
        {
            Task resultIdentity = _signinManager.SignOutAsync();
            if (resultIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou");
        }
    }
}
