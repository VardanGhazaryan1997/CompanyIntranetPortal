using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class EFormsController : Controller
    {
        private IUserService _userService;
        private IEFormsService _eFormsService;

        public EFormsController(IUserService userService, IEFormsService eFormsService)
        {
            _userService = userService;
            _eFormsService = eFormsService;
        }
        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            var allApplications = await _eFormsService.GetAllApplications(userId);
            return View(allApplications);
        }

        public IActionResult BankAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BankAccount(BankAcountViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _eFormsService.BankAccountChange(userId, model.BankName, model.NewAccountNumber, model.Reason);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Vacation()
        {
            var users = await _userService.GetUsers();
            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            ViewBag.Users = users.Where(u => u.Id != userId).Select(u => new SelectListItem { Value = u.Id.ToString(), Text = string.Join(" ", u.FirstName, u.LastName) });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Vacation(VacationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _eFormsService.CreateVacationApplication(userId, model.StartDate, model.DaysCount, model.Substitute);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DayOff()
        {
            var users = await _userService.GetUsers();
            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            ViewBag.Users = users.Where(u => u.Id != userId).Select(u => new SelectListItem { Value = u.Id.ToString(), Text = string.Join(" ", u.FirstName, u.LastName) });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DayOff(DayOffViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _eFormsService.CreateDayOffApplication(userId, model.Date, model.Substitute);

            return RedirectToAction("Index");
        }

        public IActionResult JobLeft()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> JobLeft(JobLeftViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            int userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _eFormsService.CreateJobLeftApplication(userId, model.Date, model.Reason);

            return RedirectToAction("Index");
        }
    }
}
