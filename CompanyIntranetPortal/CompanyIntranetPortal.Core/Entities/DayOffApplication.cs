namespace CompanyIntranetPortal.Core.Entities
{
    public class DayOffApplication: ApplicationBase
    {
        public DateTime Date { get; set; }
        public int Substitute { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
