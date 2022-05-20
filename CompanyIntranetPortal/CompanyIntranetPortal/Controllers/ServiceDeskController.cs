using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Services;
using CompanyIntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyIntranetPortal.Controllers
{
    [Authorize]
    public class ServiceDeskController : Controller
    {
        private IServiceDeskService _serviceDeskService;

        public ServiceDeskController(IServiceDeskService serviceDeskService)
        {
            _serviceDeskService = serviceDeskService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            List<Ticket> tickets = await _serviceDeskService.GetUserServiceTickets(userId);
            return View(tickets);
        }

        public async Task<IActionResult> CreateTicket()
        {
            List<TicketType> ticketTypes = await _serviceDeskService.GetTicketTypes();
            ViewBag.TicketTypes = ticketTypes.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketViewModel ticket)
        {
            var userId = Convert.ToInt32(ControllerContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

            await _serviceDeskService.CreateTicket(
                ticket.ContactPhone,
                ticket.Description,
                issueId: ticket.TicketIssue,
                typeid: ticket.TicketType,
                userId
                );

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
             var ticket = await _serviceDeskService.GetTicket(id);
            return View(ticket);
        }


        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> GetIssues(int id)
        {
            List<TicketIssue> issues = await _serviceDeskService.GetIssues(id);
            return Ok(issues);
        }
    }
}
