namespace Eclipseworks.Tasks.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}