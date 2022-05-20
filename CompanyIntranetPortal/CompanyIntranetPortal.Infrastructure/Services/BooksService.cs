using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class BooksService : BaseService, IBooksService
    {
        public BooksService(CompanyIntranetDBContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateBook(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Book?> GetBook(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task Update(int id, string authors, bool isAvailable, string name, int pageCount, DateTime publishedOn, string? imgUrl)
        {
            var book = await GetBook(id);
            book.Updated = DateTime.Now;
            book.Authors = authors;
            book.IsAvailable = isAvailable;
            book.Name = name;
            book.PageCount = pageCount;
            book.PublishedOn = publishedOn;
            book.ImgUrl = imgUrl ?? book.ImgUrl;

            _dbContext.Update(book);

            await _dbContext.SaveChangesAsync();
        }
    }
}
