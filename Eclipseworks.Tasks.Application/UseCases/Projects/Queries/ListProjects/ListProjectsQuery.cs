using Eclipseworks.Tasks.Domain.Entities;
using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Projects.Queries.ListProjects
{
    public class ListProjectsQuery : IRequest<IEnumerable<ProjectNameDTO>>
    {
    }
}