using Eclipseworks.Tasks.Application.Configuration;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Application.Security;
using Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.CreateTask;
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
    public class CreateTaskUnitTest
    {
        private ServiceCollection sc;

        public CreateTaskUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(CreateTaskCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });

            Mock<IOptions<AppOptions>> configMock = new(MockBehavior.Strict);
            configMock
                .SetupGet(s => s.Value)
                .Returns(new AppOptions { MaxTaskInProject = 3 });

            Mock<IAuthentication> authMock = new(MockBehavior.Strict);
            authMock.Setup(t=>t.GetUser()).Returns(new User { Email = "teste@teste.com.br" });

            Mock<IHistoryTaskRepository> histMock = new(MockBehavior.Strict);
            histMock.Setup(t => t.Create(It.IsAny<HistoryTask>())).Verifiable();

            Mock<IUserRepository> userMock = new(MockBehavior.Strict);
            userMock
                .Setup(t => t.GetByKeyAsync(It.IsAny<string>()))
                .Returns(System.Threading.Tasks.Task.FromResult(new User { Email = "teste@teste.com.br" }));

            sc.AddTransient(impl => configMock.Object);
            sc.AddTransient(impl => authMock.Object);
            sc.AddTransient(impl => histMock.Object);
            sc.AddTransient(impl => userMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTaskWithSuccessfully()
        {
            var project = new Project { Id = Guid.NewGuid(), Name = "Project 1" };
            var command = new CreateTaskCommand
            {
                Title = "Test",
                Description = "Test",
                Deadline = DateTime.Now,
                Priority = Domain.Enums.PriorityTask.High,
                ProjectId = project.Id,
                Status = Domain.Enums.StatusTask.Todo
            };

            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoProjectMock = new(MockBehavior.Strict);
            repoProjectMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(project));

            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            var tasksRegistreds = new Domain.Entities.Task[0] as IEnumerable<Domain.Entities.Task>;
            repoTaskMock
                .Setup(s => s.ListAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(tasksRegistreds));

            repoTaskMock
                .Setup(s => s.Create(It.IsAny<Domain.Entities.Task>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoProjectMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await _mediator.Send(command);

            repoTaskMock.Verify(s => s.Create(It.IsAny<Domain.Entities.Task>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationExceptionWithoutName()
        {
            var project = new Project { Id = Guid.NewGuid(), Name = "Project 1" };
            var command = new CreateTaskCommand
            {
                Title = "",
                Description = "Test",
                Deadline = DateTime.Now,
                Priority = Domain.Enums.PriorityTask.High,
                ProjectId = project.Id,
                Status = Domain.Enums.StatusTask.Todo
            };

            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoProjectMock = new(MockBehavior.Strict);
            repoProjectMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(project));

            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            var tasksRegistreds = new Domain.Entities.Task[0] as IEnumerable<Domain.Entities.Task>;
            repoTaskMock
                .Setup(s => s.ListAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(tasksRegistreds));

            repoTaskMock
                .Setup(s => s.Create(It.IsAny<Domain.Entities.Task>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoProjectMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(() => _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationExceptionWithoutProjectId()
        {
            var project = new Project { Id = Guid.Empty };
            var command = new CreateTaskCommand
            {
                Title = "Title 1",
                Description = "Test",
                Deadline = DateTime.Now,
                Priority = Domain.Enums.PriorityTask.High,
                ProjectId = project.Id,
                Status = Domain.Enums.StatusTask.Todo
            };

            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoProjectMock = new(MockBehavior.Strict);
            repoProjectMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(project));

            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            var tasksRegistreds = new Domain.Entities.Task[0] as IEnumerable<Domain.Entities.Task>;
            repoTaskMock
                .Setup(s => s.ListAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(tasksRegistreds));

            repoTaskMock
                .Setup(s => s.Create(It.IsAny<Domain.Entities.Task>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoProjectMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(async () => await _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationExceptionWithTasksAboveMax()
        {
            var project = new Project { Id = Guid.NewGuid(), Name = "Project 1" };
            var command = new CreateTaskCommand
            {
                Title = "title 1",
                Description = "Test",
                Deadline = DateTime.Now,
                Priority = Domain.Enums.PriorityTask.High,
                ProjectId = project.Id,
                Status = Domain.Enums.StatusTask.Todo
            };

            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoProjectMock = new(MockBehavior.Strict);
            repoProjectMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(project));

            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            var tasksRegistreds = new Domain.Entities.Task[] {
                new Domain.Entities.Task { Id = Guid.NewGuid() },
                new Domain.Entities.Task { Id = Guid.NewGuid() },
                new Domain.Entities.Task { Id = Guid.NewGuid() },
                new Domain.Entities.Task { Id = Guid.NewGuid() },
            } as IEnumerable<Domain.Entities.Task>;
            repoTaskMock
                .Setup(s => s.ListAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(tasksRegistreds));

            repoTaskMock
                .Setup(s => s.Create(It.IsAny<Domain.Entities.Task>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoProjectMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(() => _mediator.Send(command));
        }
    }
}