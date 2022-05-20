namespace CompanyIntranetPortal.Core.Entities
{
    public class TicketIssue : BaseEntity
    {
        public string Name { get; set; }
        public TicketType? TicketType { get; set; }
    }
}
