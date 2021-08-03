using LivMoneyAPI.Model.Authentication;
using Microsoft.EntityFrameworkCore;

namespace LivMoneyAPI.Data
{
    public class DataContext:DbContext
    {
          public DataContext (DbContextOptions<DataContext> options) : base (options) { }

           public DbSet<AuthUser> AuthUsers { get; set; }
             protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);
        }
    }
}