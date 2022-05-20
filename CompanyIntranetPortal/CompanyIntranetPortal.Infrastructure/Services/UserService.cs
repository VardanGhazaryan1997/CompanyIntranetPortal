using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;
using CompanyIntranetPortal.Infrastructure.Data;
using EnrollmentPortal.Infrastructure.Encryption;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class UserService : BaseService, IUserService
    {
        private IEncryptionService _encriptionService;

        public UserService(CompanyIntranetDBContext dbContext, IEncryptionService encryptionService) : base(dbContext)
        {
            _encriptionService = encryptionService;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(u => u.Email == email && u.IsDeleted == false).Include(u => u.Roles).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetJubilees()
        {
            var users = await _dbContext.Users.Where(u => u.DateOfBirth.Date == DateTime.Now.Date && u.IsDeleted == false).ToListAsync();
            if (users.Count == 0)
            {
                users.Add(new User { FirstName = "Vardan", LastName = "Ghazaryan" });
                users.Add(new User { FirstName = "Armen", LastName = "Petrosyan" });
                users.Add(new User { FirstName = "Karapet", LastName = "Poxosyan" });
                users.Add(new User { FirstName = "Vazgush", LastName = "Armenakyan" });
                users.Add(new User { FirstName = "Karine", LastName = "Andreasyan" });
                users.Add(new User { FirstName = "Tigran", LastName = "Karapetyan" });
            }

            return users;
        }


        public async Task<List<User>> GetNewMembers()
        {
            var users = await _dbContext.Users.Where(u => u.Created > DateTime.Now.Date.AddDays(-14)).ToListAsync();
            if (users.Count == 0)
            {
                users.Add(new User { FirstName = "Vardan", LastName = "Ghazaryan" });
                users.Add(new User { FirstName = "Armen", LastName = "Petrosyan" });
                users.Add(new User { FirstName = "Karapet", LastName = "Poxosyan" });
                users.Add(new User { FirstName = "Vazgush", LastName = "Armenakyan" });
                users.Add(new User { FirstName = "Karine", LastName = "Andreasyan" });
                users.Add(new User { FirstName = "Tigran", LastName = "Karapetyan" });
            }

            return users;
        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);
            var hashedPassword = _encriptionService.CreatePasswordHash(password);
            if (hashedPassword == user.Password)
            {
                return user;
            }

            return null;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.Include(u => u.Roles).Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _dbContext.Users.Include(u => u.Roles).Where(u => u.IsDeleted == false && u.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(User user, List<Roles> roles)
        {
            user.Roles = new List<Role>();
            foreach (var item in roles)
            {
                var role = await _dbContext.Roles.FindAsync((int)item);
                user.Roles.Add(role);
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(int id, User model, List<Roles> roles)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) return;

            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.IsActive = model.IsActive;
            user.DateOfBirth = model.DateOfBirth;

            user.Roles = new List<Role>();
            foreach (var item in roles)
            {
                var role = await _dbContext.Roles.FindAsync((int)item);
                user.Roles.Add(role);
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            user.IsDeleted = true;

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
