using CompanyIntranetPortal.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public string Description { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Display(Name = "State")]
        public TicketStates TicketState { get; set; }

        [Display(Name = "Type")]
        public TicketType? TicketType { get; set; }

        [Display(Name = "Issue")]
        public TicketIssue? TicketIssue { get; set; }
        public User? User { get; set; }
    }
}
