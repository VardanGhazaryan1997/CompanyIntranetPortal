using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Models;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IPostService
    {
        Task CratePost(string? title, string? content, string? shortDescription, string imagePath);
        Task<List<Post>> GetPosts();
        Task<Post?> GetPost(int id);
        Task<List<PostWithLikes>?> GetPostsWithLikes(int userId);
        Task LikePost(int id, int userId);
        Task DeletePost(int id);
        Task UpdatePost(int id, string? content, string fileUrl, string? shortDescription, string? title);
    }
}
