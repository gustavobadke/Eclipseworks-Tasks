namespace Eclipseworks.Tasks.Domain.Exceptions
{
    internal class NotConfiguredException : Exception
    {
        public NotConfiguredException(string message) : base(message)
        {
        }
    }
}