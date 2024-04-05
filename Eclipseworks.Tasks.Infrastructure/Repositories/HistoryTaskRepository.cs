using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Tasks.Infrastructure.Repositories
{
    public class HistoryTaskRepository : GenericRepository<HistoryTask>, IHistoryTaskRepository
    {
        public HistoryTaskRepository(AppDbContext context) : base(context)
        {
                
        }

        public async Task<IEnumerable<HistoryTask>> ListAsync(Domain.Entities.Task task)
        {
            return await _context.Histories.Where(h => h.Task.Id.Equals(task.Id)).ToListAsync();
        }
    }
}