namespace CompanyIntranetPortal.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerifed { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

    }
}
