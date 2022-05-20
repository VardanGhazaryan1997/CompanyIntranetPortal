namespace CompanyIntranetPortal.Core.Entities
{
    public class JobLeftApplication: ApplicationBase
    {
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
