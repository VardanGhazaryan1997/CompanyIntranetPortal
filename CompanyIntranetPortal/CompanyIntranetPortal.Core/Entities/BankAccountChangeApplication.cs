namespace CompanyIntranetPortal.Core.Entities
{
    public class BankAccountChangeApplication: ApplicationBase
    {
        public string NewAccountNumber { get; set; }
        public string BankName { get; set; }
        public string Reason { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
