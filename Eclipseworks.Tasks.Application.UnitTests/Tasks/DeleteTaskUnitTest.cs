using Eclipseworks.Tasks.Application.Exceptions;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.DeleteTask;
using Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.UpdateTask;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Eclipseworks.Tasks.Application.UnitTests.Projects
{
    public class DeleteTaskUnitTest
    {
        private ServiceCollection sc;

        public DeleteTaskUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(UpdateTaskCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });


            var histories = new HistoryTask[] { new HistoryTask() } as IEnumerable<HistoryTask>;
            Mock<IHistoryTaskRepository> histMock = new(MockBehavior.Strict);
            histMock
                .Setup(t => t.ListAsync(It.IsAny<Domain.Entities.Task>()))
                .Returns(System.Threading.Tasks.Task.FromResult(histories));

            histMock
                .Setup(t => t.Delete(It.IsAny<HistoryTask>()))
                .Verifiable();

            sc.AddTransient(impl => histMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTaskWithSuccessfully()
        {
            Domain.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(task.Id);

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await _mediator.Send(command);

            repoTaskMock.Verify(s => s.Delete(It.IsAny<Domain.Entities.Task>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationExceptionWithoutId()
        {
            Domain.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(Guid.Empty);

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(async () => await _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowNotFoundTask()
        {
            Domain.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(Guid.NewGuid());

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<NotFoundException>(async () => await _mediator.Send(command));
        }

        private Domain.Entities.Task GetTaskToDelete()
        {
            return new Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                Deadline = DateTime.Now,
                Priority = Domain.Enums.PriorityTask.Medium,
                Status = Domain.Enums.StatusTask.Doing,
                Project = new Project()
            };
        }

        private Mock<ITaskRepository> GetTaskRepository(Domain.Entities.Task task)
        {
            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            repoTaskMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult((Domain.Entities.Task)null));

            repoTaskMock
                .Setup(s => s.GetByIdAsync(task.Id))
                .Returns(System.Threading.Tasks.Task.FromResult(task));

            repoTaskMock
                .Setup(s => s.Delete(It.IsAny<Domain.Entities.Task>()))
                .Verifiable();

            return repoTaskMock;
        }

        private Mock<IUnitOfWork> GetUnitOfWork()
        {
            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);
            return uowMock;
        }
    }
}