namespace CompanyIntranetPortal.Models
{
    public class PostViewModel
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public IFormFile? PostImage { get; set; }
    }   
}
