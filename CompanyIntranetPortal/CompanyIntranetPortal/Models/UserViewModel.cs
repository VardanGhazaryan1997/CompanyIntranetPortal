using CompanyIntranetPortal.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public List<Roles> Roles { get; set; }

        public bool IsActive { get; set; }
        public bool IsVerifed { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

    }
}
