using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Models.Users;
using src.Persistence.Repositories.Users.Interfaces;

namespace src.Persistence.Repositories.Users.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
        public async Task AddAsync(User user, ApplicationRole[] userRoles)
        {
            var roleNames = userRoles.Select(r => r.ToString()).ToList();
            var roles = await _context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            _context.Users.Add(user);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                        .ThenInclude(ur => ur.Role)
                                        .SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}