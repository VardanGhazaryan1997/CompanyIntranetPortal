using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        [Display(Name = "Type")]
        public int ApplicationType { get; set; }
    }
}
