using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class VacationViewModel
    {
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Days Count")]
        public int DaysCount { get; set; }

        public int Substitute { get; set; }
    }
}
