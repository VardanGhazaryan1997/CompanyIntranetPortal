using CompanyIntranetPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    public class VacanciesController : Controller
    {
        private IVacancyService _vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _vacancyService.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _vacancyService.GetVacancy(id));
        }
    }
}
