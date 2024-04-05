using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Tasks.Queries.ListTasks
{
    public class ListTasksQuery : IRequest<IEnumerable<Domain.Entities.Task>>
    {
        public ListTasksQuery(Guid projectId)
        {
            this.ProjectId = projectId;
        }

        public Guid ProjectId { get; set; }
    }
}