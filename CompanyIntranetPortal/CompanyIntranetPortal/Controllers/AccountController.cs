using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyIntranetPortal.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid) return View(loginModel);
            var user = await _userService.GetUserByEmail(loginModel.Email);


            if (user == null || !user.IsActive)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(loginModel);
            }

            user = await _userService.Login(loginModel.Email, loginModel.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(loginModel);
            }

            await Authenticate(user, loginModel.RememberMe);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private async Task Authenticate(User user, bool isPersistent)
        {
            try
            {
                var roles = user.Roles;

                var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("Roles", string.Join(',', roles.Select(r=>r.RoleName))),
            new Claim("Name", string.Join(' ', user.FirstName,user.LastName))
        };

                var authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = isPersistent,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    AllowRefresh = true
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id),
                    authenticationProperties);
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
                throw;
            }
        }
    }
}
