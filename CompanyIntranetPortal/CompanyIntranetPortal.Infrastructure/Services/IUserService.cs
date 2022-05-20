using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> Login(string email, string password);
        Task<List<User>> GetJubilees();
        Task<List<User>> GetNewMembers();
        Task<List<User>> GetUsers();
        Task Create(User model, List<Core.Enums.Roles> roles);
        Task<User> GetUser(int id);
        Task UpdateUser(int id, User user, List<Core.Enums.Roles> roles);
        Task DeleteUser(int id);
    }
}
