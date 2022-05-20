namespace CompanyIntranetPortal.Infrastructure.Models
{
    public class PostWithLikes
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImgUrl { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked{ get; set; }
    }
}
