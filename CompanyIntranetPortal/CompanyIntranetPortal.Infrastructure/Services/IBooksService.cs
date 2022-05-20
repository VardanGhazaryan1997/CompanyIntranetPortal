using CompanyIntranetPortal.Core.Entities;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IBooksService
    {
        Task<List<Book>> GetBooks();
        Task CreateBook(Book book);
        Task<Book?> GetBook(int id);
        Task DeleteBook(int id);
        Task Update(int id, string authors, bool isAvailable, string name, int pageCount, DateTime publishedOn, string? imgUrl);
    }
}
