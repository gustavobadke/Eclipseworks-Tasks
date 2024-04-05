using AutoMapper;
using AutoMapper.QueryableExtensions;
using Eclipseworks.Tasks.Domain.Entities;
using Eclipseworks.Tasks.Domain.Repository;
using Eclipseworks.Tasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Tasks.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public new async Task<IEnumerable<ProjectNameDTO>> ListAsync()
        {
            return await _context.Projects
                .ProjectTo<ProjectNameDTO>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}