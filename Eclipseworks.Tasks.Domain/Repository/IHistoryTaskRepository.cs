using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Domain.Repository
{
    public interface IHistoryTaskRepository
    {
        void Create(HistoryTask task);

        void Delete(HistoryTask task);

        Task<IEnumerable<HistoryTask>> ListAsync(Domain.Entities.Task task);

    }
}