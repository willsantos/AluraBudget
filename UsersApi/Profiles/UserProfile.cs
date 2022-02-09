using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.DTO;
using UsersApi.Models;

namespace UsersApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
