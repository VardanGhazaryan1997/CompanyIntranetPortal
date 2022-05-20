using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Data;
using CompanyIntranetPortal.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class PostService : BaseService, IPostService
    {
        public PostService(CompanyIntranetDBContext dbContext) : base(dbContext)
        {
        }

        public async Task CratePost(string? title, string? content, string? shortDescription, string imagePath)
        {
            var newPost = new Post();
            newPost.Title = title;
            newPost.ShortDescription = shortDescription;
            newPost.Content = content;
            newPost.ImgUrl = imagePath;

            await _dbContext.Posts.AddAsync(newPost);
            var resutl = await _dbContext.SaveChangesAsync();

        }

        public async Task DeletePost(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            _dbContext.Posts.Remove(post);
            _dbContext.PostLikes.RemoveRange(_dbContext.PostLikes.Where(pl => pl.PostId == id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Post?> GetPost(int id)
        {
            return await _dbContext.Posts.FindAsync(id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<List<PostWithLikes>?> GetPostsWithLikes(int userId)
        {
            var result = new List<PostWithLikes>();
            var posts = await _dbContext.Posts.OrderByDescending(p=>p.Created).ToListAsync();

            foreach (var post in posts)
            {
                var userLiked = await _dbContext.PostLikes.Where(pl => pl.User.Id == userId && pl.Post.Id == post.Id).FirstOrDefaultAsync() != null;
                result.Add(new PostWithLikes
                {
                    Id = post.Id,
                    Content = post.Content,
                    ImgUrl = post.ImgUrl,
                    IsLiked = userLiked,
                    LikesCount = post.LikesCount,
                    ShortDescription = post.ShortDescription,
                    Title = post.Title
                });
            }

            return result;
        }

        public async Task LikePost(int postId, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            var post = await _dbContext.Posts.FindAsync(postId);
            var postLikes = await _dbContext.PostLikes.Where(pl => pl.User.Id == userId && pl.PostId == postId).FirstOrDefaultAsync();
            if (postLikes == null && user != null && post != null)
            {
                var postLike = new PostLike { PostId = postId, Created = DateTime.Now, User = user, Post = post };
                await _dbContext.PostLikes.AddAsync(postLike);
                post.LikesCount++;
                _dbContext.Posts.Update(post);

                await _dbContext.SaveChangesAsync();
            }
            else
            {
             var postLike =   await _dbContext.PostLikes.Where(pl => pl.PostId == postId && pl.User.Id == userId).FirstOrDefaultAsync();
             _dbContext.PostLikes.Remove(postLike);
             post.LikesCount--;
             _dbContext.Posts.Update(post);
             await _dbContext.SaveChangesAsync();
            }

        }

        public async Task UpdatePost(int postId, string? content, string fileUrl, string? shortDescription, string? title)
        {
            var post = await _dbContext.Posts.FindAsync(postId);

            post.Content = content;
            post.Title = title;
            post.ShortDescription = shortDescription;
            post.ImgUrl = fileUrl;

            _dbContext.Update(post);

            await _dbContext.SaveChangesAsync();
        }
    }
}
