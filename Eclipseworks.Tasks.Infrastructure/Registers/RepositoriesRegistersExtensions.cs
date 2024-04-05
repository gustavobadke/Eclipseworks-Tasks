using Eclipseworks.Tasks.Application.Configuration;
using Eclipseworks.Tasks.Application.Repositories;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Infrastructure.Persistence;
using Eclipseworks.Tasks.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eclipseworks.Tasks.Infrastructure.Registers
{
    public static class RepositoriesRegistersExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, AppOptions options)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(options.ConnectionString));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentTaskRepository, CommentTaskRepository>();
            services.AddScoped<IHistoryTaskRepository, HistoryTaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}