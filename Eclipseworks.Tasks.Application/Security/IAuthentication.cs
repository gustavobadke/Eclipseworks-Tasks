using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Application.Security
{
    public interface IAuthentication
    {
        public User GetUser();
    }
}