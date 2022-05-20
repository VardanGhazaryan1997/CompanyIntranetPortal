
namespace CompanyIntranetPortal.Core.Entities
{
    public class Vacancy:BaseEntity
    {
        public string PositionName { get; set; }
        public string JobDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}
