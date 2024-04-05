using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Domain.Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetByKeyAsync(string key);
    }
}