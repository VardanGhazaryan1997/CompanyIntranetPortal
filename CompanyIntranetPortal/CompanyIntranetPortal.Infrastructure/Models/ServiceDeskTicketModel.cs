using CompanyIntranetPortal.Core.Enums;

namespace CompanyIntranetPortal.Infrastructure.Models
{
    public class ServiceDeskTicketModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ContactPhone { get; set; }
        public TicketStates TicketState { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
