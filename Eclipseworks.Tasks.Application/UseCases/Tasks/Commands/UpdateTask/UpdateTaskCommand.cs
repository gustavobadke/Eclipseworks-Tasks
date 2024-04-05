using Eclipseworks.Tasks.Domain.Enums;
using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTask Status { get; set; }
    }
}