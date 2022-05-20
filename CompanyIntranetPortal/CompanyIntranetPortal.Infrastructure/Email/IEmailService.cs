namespace CompanyIntranetPortal.Infrastructure
{
    public interface IEmailService
    {
        public void SendEmail(string to, string subject, string message);
    }
}
