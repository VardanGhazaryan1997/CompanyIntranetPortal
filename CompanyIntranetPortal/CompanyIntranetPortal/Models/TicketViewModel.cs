using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Display(Name ="Type")]
        public int TicketType { get; set; }

        [Display(Name = "Issue")]
        public int TicketIssue { get; set; }
    }
}
