using AutoMapper;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Infrastructure.Persistence;

namespace Eclipseworks.Tasks.Infrastructure.Repositories
{
    public class CommentTaskRepository : GenericRepository<CommentTask>, ICommentTaskRepository
    {
        public CommentTaskRepository(AppDbContext context) : base(context) { }
    }
}