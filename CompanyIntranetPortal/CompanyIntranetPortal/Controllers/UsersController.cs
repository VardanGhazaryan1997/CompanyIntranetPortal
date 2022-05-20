using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IEmailService _emailService;

        public UsersController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await _userService.GetUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                IsActive = true,
                Email = model.Email
            };

            await _userService.Create(user, model.Roles);

            _emailService.SendEmail(user.Email, "Welcome to Company Intranet", "");

            return RedirectToAction("Index", "Users");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUser(id);
            return View(user);
        }

        public async Task<IActionResult> Edith(int id)
        {
            var user = await _userService.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edith(int id, UserViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                IsActive = true,
            };

            await _userService.UpdateUser(id, user, model.Roles);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user  = await _userService.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
