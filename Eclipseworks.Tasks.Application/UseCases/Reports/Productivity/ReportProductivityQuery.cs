using MediatR;

namespace Eclipseworks.Tasks.Application.UseCases.Reports.Productivity
{
    public class ReportProductivityQuery : IRequest<ReportProductivityDTO>
    {
        public ReportProductivityQuery(int quantityLastDays)
        {
            QuantityLastDays = quantityLastDays;
        }

        public int QuantityLastDays { get; set; }
    }
}