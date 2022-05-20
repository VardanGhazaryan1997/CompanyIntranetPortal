using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Core.Entities
{
    public class Book : BaseEntity
    {
        [Display(Name = "Book Title")]
        public string Name { get; set; }
        public string Authors { get; set; }

        [Display(Name = "Page Count")]
        public int PageCount { get; set; }


        [Display(Name = "PublishedOn")]
        public DateTime PublishedOn{ get; set; }
        public bool IsAvailable{ get; set; }
        public string? ImgUrl { get; set; }
    }
}
