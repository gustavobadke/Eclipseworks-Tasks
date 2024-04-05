using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Domain.Repository
{
    public interface IProjectRepository
    {
        void Create(Project project);
        void Delete(Project project);
        Task<Project?> GetByIdAsync(Guid projectId);
        Task<IEnumerable<ProjectNameDTO>> ListAsync();
    }
}