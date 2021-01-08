using System.Collections.Generic;
using System.Linq;
using src.Domain.Models.Users;
using src.Infrastructure.Security.Interfaces;

namespace src.Persistence.Services
{
    public class DatabaseSeed
    {
        public static void Seed(AppDbContext context, IPasswordHasher passwordHasher)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Count() == 0)
            {
                var roles = new List<Role>
                {
                    new Role { Name = ApplicationRole.User.ToString() },
                    new Role { Name = ApplicationRole.Administrator.ToString() }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                var users = new List<User>
                {
                    new User { Username = "user", Password = passwordHasher.HashPassword("1234") },
                    new User { Username = "admin", Password = passwordHasher.HashPassword("1234") },
                };

                users[0].UserRoles.Add(new UserRole
                {
                    RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRole.User.ToString()).Id
                });

                users[1].UserRoles.Add(new UserRole
                {
                    RoleId = context.Roles.SingleOrDefault(r => r.Name == ApplicationRole.Administrator.ToString()).Id
                });

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}