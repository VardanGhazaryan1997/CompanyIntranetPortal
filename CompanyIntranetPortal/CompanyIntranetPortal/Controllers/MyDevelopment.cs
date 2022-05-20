using CompanyIntranetPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class MyDevelopment : Controller
    {
        private IBooksService _booksService;

        public MyDevelopment(IBooksService booksService)
        {
            _booksService = booksService;
        }
        public async Task<IActionResult> Books()
        {
            var books = await _booksService.GetBooks();
            return View(books);
        }
    }
}
