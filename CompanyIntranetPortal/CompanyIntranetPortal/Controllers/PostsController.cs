using CompanyIntranetPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> ReadPost(int id)
        {
            return View(await _postService.GetPost(id));
        }

        public async Task<IActionResult> LikePost(int id)
        {
            var userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _postService.LikePost(id, userId);
            return RedirectToAction("Index","Home");
        }
    }
}
