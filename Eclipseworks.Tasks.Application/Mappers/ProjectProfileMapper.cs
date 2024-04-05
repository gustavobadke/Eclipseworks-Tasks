using AutoMapper;
using Eclipseworks.Tasks.Domain.Entities;

namespace Eclipseworks.Tasks.Application.Mappers
{
    public class ProjectProfileMapper : Profile
    {
        public ProjectProfileMapper()
        {
            CreateMap<Project, ProjectNameDTO>();
        }
    }
}