using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IPostService _postService;
        private IWebHostEnvironment _appEnvironment;
        private IServiceDeskService _serviceDeskService;
        private IApplicationService _applicationService;
        private IEFormsService _eFormsService;
        private IVacancyService _vacancyService;

        public AdminController(
            IPostService postService,
            IWebHostEnvironment appEnvironment,
            IServiceDeskService serviceDeskService,
            IApplicationService applicationService,
            IEFormsService eFormsService,
            IVacancyService vacancyService)
        {
            _postService = postService;
            _appEnvironment = appEnvironment;
            _serviceDeskService = serviceDeskService;
            _applicationService = applicationService;
            _eFormsService = eFormsService;
            _vacancyService = vacancyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imagePath = string.Empty;
            var uploadedFile = model.PostImage;
            if (uploadedFile != null)
            {
                imagePath = await GetFileUrl(uploadedFile);
            }
            await _postService.CratePost(model.Title, model.Content, model.ShortDescription, imagePath);

            return View();
        }


        public async Task<IActionResult> Posts()
        {
            List<Post> posts = await _postService.GetPosts();
            return View(posts);
        }

        public async Task<IActionResult> PostDetails(int id)
        {
            var post = await _postService.GetPost(id);
            return View(post);
        }

        public async Task<IActionResult> PostDelete(int id)
        {
            await _postService.DeletePost(id);
            return RedirectToAction("Posts", "Admin");
        }

        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _postService.GetPost(id);
            var model = new PostViewModel
            {
                Content = post.Content,
                ShortDescription = post.ShortDescription,
                Title = post.Title
            };

            return View("~/Views/Admin/EditPost.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> PostEdit(int id, PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var post = await _postService.GetPost(id);
            var imagePath = string.Empty;
            await _postService.UpdatePost(id, model.Content, await GetFileUrl(model.PostImage), model.ShortDescription, model.Title);
            return RedirectToAction("Posts", "Admin");
        }

        public async Task<IActionResult> ServiceDesk()
        {
            var tickets = await _serviceDeskService.GetAllTickets();
            return View(tickets);
        }

        public async Task<IActionResult> TicketDetails(int id)
        {
            return View(await _serviceDeskService.GetTicket(id));
        }

        public async Task<IActionResult> TicketDelete(int id)
        {
            return View(await _serviceDeskService.GetTicket(id));
        }

        public async Task<IActionResult> TicketDeleteConfirm(int id)
        {
            await _serviceDeskService.DeleteTicket(id);
            return RedirectToAction("ServiceDesk");
        }
        public async Task<IActionResult> Applications()
        {
            var applications = await _applicationService.GetApplications();
            return View(applications);
        }

        public async Task<IActionResult> ApplicationDetails(int id)
        {
            var application = await _applicationService.GetApplication(id);
            return View(application);
        }

        public async Task<IActionResult> EForms()
        {
            return View(await _eFormsService.GetVacationRequests());
        }

        public async Task<IActionResult> DayoffRequests()
        {
            return View(await _eFormsService.GetDayoffRequests());
        }
        public async Task<IActionResult> JobLeftRequests()
        {
            return View(await _eFormsService.GetJobLeftRequests());
        }
        public async Task<IActionResult> BankAccountChangeRequests()
        {
            return View(await _eFormsService.GetBankAccountChangeRequests());
        }

        public async Task<IActionResult> VocationRequestDelete(int id)
        {
            var request = await _eFormsService.GetVacationRequest(id);
            return View(request);
        }

        public async Task<IActionResult> VocationRequestDeleteConfirm(int id)
        {
            await _eFormsService.VocationRequestDelete(id);
            return RedirectToAction("EForms");
        }

        public async Task<IActionResult> DayoffRequestDelete(int id)
        {
            var request = await _eFormsService.GetDayoffRequest(id);
            return View(request);
        }

        public async Task<IActionResult> DayoffRequestDeleteConfirm(int id)
        {
            await _eFormsService.DayoffRequestDelete(id);
            return RedirectToAction("DayoffRequests");
        }

        public async Task<IActionResult> JobLeftRequestDelete(int id)
        {
            var request = await _eFormsService.GetJobLeftRequest(id);
            return View(request);
        }

        public async Task<IActionResult> JobLeftRequestDeleteConfirm(int id)
        {
            await _eFormsService.JobLeftRequestDelete(id);
            return RedirectToAction("JobLeftRequests");
        }

        public async Task<IActionResult> BankAccountChangeRequestDelete(int id)
        {
            var request = await _eFormsService.GetBankAccountChangeRequest(id);
            return View(request);
        }

        public async Task<IActionResult> BankAccountChangeRequestDeleteConfirm(int id)
        {
            await _eFormsService.BankAccountChangeRequestDelete(id);
            return RedirectToAction("BankAccountChangeRequests");
        }

        public async Task<IActionResult> VocationUpdateState(int id, ApplicationState state)
        {
            await _eFormsService.VocationUpdate(id, state);
            return RedirectToAction("EForms");
        }

        public async Task<IActionResult> DayoffUpdateState(int id, ApplicationState state)
        {
            await _eFormsService.DayoffUpdate(id, state);
            return RedirectToAction("DayoffRequests");
        }

        public async Task<IActionResult> JobLeftUpdateState(int id, ApplicationState state)
        {
            await _eFormsService.JobLeftUpdate(id, state);
            return RedirectToAction("JobLeftRequests");
        }

        public async Task<IActionResult> BankAccountUpdateState(int id, ApplicationState state)
        {
            await _eFormsService.BankAccountUpdate(id, state);
            return RedirectToAction("BankAccountChangeRequests");
        }

        public async Task<IActionResult> ApplicationUpdateState(int id, ApplicationState state)
        {
            await _eFormsService.ApplicationUpdate(id, state);
            return RedirectToAction("Applications");
        }

        public async Task<IActionResult> TicketUpdateState(int id, TicketStates state)
        {
            await _eFormsService.TicketUpdate(id, state);
            return RedirectToAction("ServiceDesk");
        }

        public async Task<IActionResult> ApplicationDelete(int id)
        {
            var request = await _applicationService.GetApplication(id);
            return View(request);
        }

        public async Task<IActionResult> ApplicationDeleteConfirm(int id)
        {
            await _applicationService.DeleteGetApplication(id);
            return RedirectToAction("Applications");
        }

        public async Task<IActionResult> Vacancies()
        {
            List<Vacancy> vacancies = await _vacancyService.GetAll();
            return View(vacancies);
        }

        public async Task<IActionResult> VacancyCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VacancyCreate(Vacancy model)
        {
            await _vacancyService.CreateVacancy(model);
            return RedirectToAction("Vacancies");
        }

        public async Task<IActionResult> VacancyEdit(int id)
        {
            var vacancy = await _vacancyService.GetVacancy(id);
            return View(vacancy);
        }

        [HttpPost]
        public async Task<IActionResult> VacancyEdit(int id, Vacancy model)
        {
            await _vacancyService.UpdateVacancy(id, model);
            return RedirectToAction("Vacancies");
        }

        public async Task<IActionResult> VacancyDetails(int id)
        {
            var vacancy = await _vacancyService.GetVacancy(id);
            return View(vacancy);
        }

        public async Task<IActionResult> VacancyDelete(int id)
        {
            var vacancy = await _vacancyService.GetVacancy(id);
            return View(vacancy);
        }

        public async Task<IActionResult> VacancyDeleteConfirm(int id)
        {
            await _vacancyService.DeleteVacancy(id);
            return RedirectToAction("Vacancies");
        }


        private async Task<string> GetFileUrl(IFormFile? uploadedFile)
        {
            string imagePath = "/images/Posts/" + Guid.NewGuid().ToString() + uploadedFile.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + imagePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return imagePath;
        }

    }
}
