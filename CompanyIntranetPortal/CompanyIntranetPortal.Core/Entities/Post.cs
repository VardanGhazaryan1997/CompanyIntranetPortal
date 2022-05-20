namespace CompanyIntranetPortal.Core.Entities
{
    public class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImgUrl { get; set; }
        public int LikesCount { get; set; }
        public ICollection<PostLike>? PostLikes { get; set; }
    }
}
