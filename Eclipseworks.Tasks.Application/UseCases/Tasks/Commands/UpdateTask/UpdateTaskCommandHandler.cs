using Eclipseworks.Tasks.Application.Exceptions;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Application.Security;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Domain.VOs;
using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.UpdateTask
{
    public class UdpateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IHistoryTaskRepository _historyTaskRepository;
        private readonly IAuthentication _authentication;

        public UdpateTaskCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository, IHistoryTaskRepository historyTaskRepository, IAuthentication authentication)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _historyTaskRepository = historyTaskRepository;
            _authentication = authentication;
        }

        public async System.Threading.Tasks.Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null) throw new NotFoundException("Task not found");
            
            var oldTask = (Domain.Entities.Task)task.Clone();

            task.Title = request.Title;
            task.Deadline = request.Deadline;
            task.Description = request.Description;
            task.Status = request.Status;

            _taskRepository.Update(task);
            
            var changes = new HistoryTaskChanges(oldTask, task);
            var history = new HistoryTask
            {
                Id = Guid.NewGuid(),
                Actor = _authentication.GetUser(),
                GeneratedAt = DateTime.Now,
                Task = task,
                Content = changes.ToString()
            };

            _historyTaskRepository.Create(history);

            await _uow.SaveChangesAsync();
        }
    }
}