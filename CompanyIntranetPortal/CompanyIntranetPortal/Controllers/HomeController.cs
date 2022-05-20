using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Models;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger,IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            List<PostWithLikes> posts = await _postService.GetPostsWithLikes(userId);
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}