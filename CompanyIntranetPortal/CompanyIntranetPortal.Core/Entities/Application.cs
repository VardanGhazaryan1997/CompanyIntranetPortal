namespace CompanyIntranetPortal.Core.Entities
{
    public class Application : ApplicationBase
    {
        public string? Description { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public User User { get; set; }
    }
}
