using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Model.Authentication.AppRole;
using Microsoft.EntityFrameworkCore;
namespace LivMoneyAPI.Data
{
    public class DataContext:DbContext
    {
          public DataContext (DbContextOptions<DataContext> options) : base (options) { }
           public DbSet<AuthUser> AuthUsers { get; set; }
           public DbSet<AuthRole> AuthRoles { get; set; }
           public DbSet<UserRole> UserRoles { get; set; }
           protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);
        }
    }
}