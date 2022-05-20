using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private IBooksService _booksService;
        private IWebHostEnvironment _appEnvironment;

        public BooksController(IBooksService booksService, IWebHostEnvironment appEnvironment)
        {
            _booksService = booksService;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetBooks();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imageUrl = await GetFileUrl(model.BookImage);
            var book = new Book
            {
                ImgUrl = imageUrl,
                Name = model.Name,
                Authors = model.Authors,
                IsAvailable = model.IsAvailable,
                PublishedOn = model.PublishedOn,
                PageCount = model.PageCount,
            };

            await _booksService.CreateBook(book);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _booksService.GetBook(id);
            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _booksService.GetBook(id);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _booksService.DeleteBook(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _booksService.GetBook(id);
           

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Authors = book.Authors,
                IsAvailable = book.IsAvailable,
                Name = book.Name,
                PageCount = book.PageCount,
                PublishedOn = book.PublishedOn,
            };

            if (book.ImgUrl != null)
            {
                var filePath = _appEnvironment.WebRootPath + book.ImgUrl;
                using var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath).ToArray());
                var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
                viewModel.BookImage = formFile;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            string imgUrl = null;
            if (viewModel.BookImage != null)
            {
                imgUrl = await this.GetFileUrl(viewModel.BookImage);
            }


            await _booksService.Update(
                id,
                viewModel.Authors,
                viewModel.IsAvailable,
                viewModel.Name,
                viewModel.PageCount,
                viewModel.PublishedOn,
                imgUrl);
            return RedirectToAction("Index");
        }

        private async Task<string> GetFileUrl(IFormFile? uploadedFile)
        {
            string imagePath = "/images/Books/" + Guid.NewGuid().ToString() + uploadedFile.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + imagePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return imagePath;
        }
    }
}
