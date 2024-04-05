using Eclipseworks.Tasks.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipseworks.Tasks.Application.UseCases.Tasks.Queries.ListTasks
{
    public class ListTasksQueryHandler : IRequestHandler<ListTasksQuery, IEnumerable<Domain.Entities.Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public ListTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(ListTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.ListAsync(request.ProjectId);
        }
    }
}
