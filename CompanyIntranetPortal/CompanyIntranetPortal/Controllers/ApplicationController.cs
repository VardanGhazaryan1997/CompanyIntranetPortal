using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        public async Task<IActionResult> Index()
        {
            var applications = await _applicationService.GetApplications();
            return View(applications);
        }

        public async Task<IActionResult> Create()
        {
            List<ApplicationType> ticketTypes = await _applicationService.GetApplicationTypes();
            ViewBag.ApplicationTypes = ticketTypes.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            await _applicationService.Create(userId, model.ApplicationType, model.Description);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var application = await _applicationService.GetApplication(id);
            return View(application);
        }
    }
}
