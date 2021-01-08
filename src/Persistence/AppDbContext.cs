using Microsoft.EntityFrameworkCore;
using src.Domain.Models.Users;

namespace src.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        
        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new System.ArgumentNullException(nameof(modelBuilder));
            }
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(ur => new {ur.UserId, ur.RoleId});
        }
    }
}