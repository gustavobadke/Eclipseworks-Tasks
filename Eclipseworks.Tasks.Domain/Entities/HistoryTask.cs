namespace Eclipseworks.Tasks.Domain.Entities
{
    public class HistoryTask
    {
        public Guid Id { get; set; }
        public User Actor { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string Content { get; set; }
        public Task Task { get; set; }
    }
}