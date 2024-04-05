using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Infrastructure.Persistence;

namespace Eclipseworks.Tasks.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByKeyAsync(string key)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(key));
        }
    }
}