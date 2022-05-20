using CompanyIntranetPortal.Core.Enums;

namespace CompanyIntranetPortal.Infrastructure.Models
{
    public class ApplicationCommonModel
    {
        public DateTime Created { get; set; }
        public string Type{ get; set; }
        public ApplicationState ApplicationState { get; set; }  
    }
}
