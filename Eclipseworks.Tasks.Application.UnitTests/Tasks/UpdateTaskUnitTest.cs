using Eclipseworks.Tasks.Application.Configuration;
using Eclipseworks.Tasks.Application.Exceptions;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Application.Security;
using Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.UpdateTask;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace Eclipseworks.Tasks.Application.UnitTests.Projects
{
    public class UpdateTaskUnitTest
    {
        private ServiceCollection sc;

        public UpdateTaskUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(UpdateTaskCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });

            Mock<IOptions<AppOptions>> configMock = new(MockBehavior.Strict);
            configMock
                .SetupGet(s => s.Value)
                .Returns(new AppOptions { MaxTaskInProject = 3 });

            Mock<IHistoryTaskRepository> histMock = new(MockBehavior.Strict);
            histMock.Setup(t => t.Create(It.IsAny<HistoryTask>())).Verifiable();

            Mock<IAuthentication> authMock = new(MockBehavior.Strict);
            authMock.Setup(t => t.GetUser()).Returns(new User { Email = "teste@teste.com.br" });

            sc.AddTransient(impl => configMock.Object);
            sc.AddTransient(impl => histMock.Object);
            sc.AddTransient(impl => authMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTaskWithSuccessfully()
        {
            Domain.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = task.Id,
                Title = "Test Updated",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Domain.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await _mediator.Send(command);

            repoTaskMock.Verify(s => s.Update(It.IsAny<Domain.Entities.Task>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidatinoExceptionWithoutId()
        {
            Domain.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = Guid.Empty,
                Title = "Test Updated",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Domain.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(async () => await _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidatinoExceptionWithoutTitle()
        {
            Domain.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = task.Id,
                Title = "",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Domain.Enums.StatusTask.Done
            };
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
            Domain.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = Guid.NewGuid(),
                Title = "Ttitle 1",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Domain.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<NotFoundException>(async () => await _mediator.Send(command));
        }

        private Domain.Entities.Task GetTaskToUpdate()
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
                .Setup(s => s.Update(It.IsAny<Domain.Entities.Task>()))
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