using AutoMapper;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Projects.Queries.ListProjects
{
    internal class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery, IEnumerable<ProjectNameDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ListProjectsQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectNameDTO>> Handle(ListProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.ListAsync();
        }
    }
}