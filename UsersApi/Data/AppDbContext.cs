using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
    }
}
