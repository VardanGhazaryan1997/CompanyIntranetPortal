using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class BankAcountViewModel
    {
        [Display(Name = "New Account Number")]
        public string NewAccountNumber { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public string Reason { get; set; }
    }
}
