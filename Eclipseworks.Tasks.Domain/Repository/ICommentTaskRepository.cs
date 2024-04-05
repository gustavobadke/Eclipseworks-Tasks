using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Domain.Repository
{
    public interface ICommentTaskRepository
    {
        public void Create(CommentTask comment);
    }
}