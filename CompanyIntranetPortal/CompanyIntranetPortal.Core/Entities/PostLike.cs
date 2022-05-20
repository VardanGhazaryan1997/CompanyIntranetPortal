
namespace CompanyIntranetPortal.Core.Entities
{
    public class PostLike:BaseEntity
    {
        public int PostId{ get; set; }
        public Post Post{ get; set; }
        public User User{ get; set; }
    }
}
