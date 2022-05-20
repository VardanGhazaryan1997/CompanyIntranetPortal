namespace CompanyIntranetPortal.Core.Entities
{
    public class VacationApplication : ApplicationBase
    {
        public DateTime StartDate { get; set; }
        public int DaysCount { get; set; }
        public int Substitute { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
