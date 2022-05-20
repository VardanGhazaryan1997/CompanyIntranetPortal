using System.ComponentModel.DataAnnotations;

namespace CompanyIntranetPortal.Models
{
    public class BookViewModel
    {

        [Display(Name = "Book Title")]
        public string Name { get; set; }

        [Display(Name = "Page Count")]
        public int PageCount { get; set; }

        [Display(Name = "PublishedOn")]
        public DateTime PublishedOn { get; set; }
        public string Authors { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? BookImage { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
