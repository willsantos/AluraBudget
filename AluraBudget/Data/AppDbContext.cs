using AluraBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraBudget.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Outgoing> Outgoings { get; set; }
    }
}
