using Eclipseworks.Tasks.Domain.Enums;

namespace Eclipseworks.Tasks.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public AccessMode AccessMode { get; set; }
    }
}
