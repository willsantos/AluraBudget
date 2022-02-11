using FluentResults;
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

        public Result SendLinkToResetPassword(SendResetPasswordRequest request)
        {
            IdentityUser<int> identityUser = GetUserByEmail(request.Email);
            if (identityUser != null)
            {
                string recoveryCode = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(recoveryCode);
            }

            return Result.Fail("Falha ao solicitar redefinição da senha");
        }

        private IdentityUser<int> GetUserByEmail(string email)
        {
            return _signInManager
                            .UserManager
                            .Users
                            .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }

        public Result ResetPassword(ResetPasswordRequest request)
        {
            IdentityUser<int> identityUser = GetUserByEmail(request.Email);

            IdentityResult identityResult = _signInManager
                .UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (identityResult.Succeeded) return Result.Ok().WithSuccess("Senha redefinida com sucesso");
            return Result.Fail("Falha na alteração");
        }
    }
}
