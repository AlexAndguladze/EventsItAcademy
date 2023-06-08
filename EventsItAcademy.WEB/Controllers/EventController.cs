using EventsItAcademy.Application.Events;
using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Tickets;
using EventsItAcademy.WEB.infrastructure.StaticItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventsItAcademy.WEB.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;

        public EventController(IEventService service, ITicketService tktService)
        {
                _eventService = service;
                _ticketService = tktService;    
        }
        public async Task<IActionResult> Index()
        {
            CancellationToken cancellationToken = CancellationToken.None;
            var events = await _eventService.GetAllActiveAsync(cancellationToken);
            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(ownerId != null) { ViewData["OwnerId"] = ownerId.ToString(); }
            else { ViewData["OwnerId"] = " "; }

            var eventEditLimitDays = SettingsSetByAdmin.GetEventEditLimitDays();
            ViewData["EditableDays"]=eventEditLimitDays.ToString();

            return View(events);
        }
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            var @event = await _eventService.GetByIdAsync(cancellationToken, id);
            if(@event == null)
            {
                return NotFound();
            }
            int soldTicketQuantity = await _ticketService.CountTakenTicketsAsync(cancellationToken, @event.Id);
            @event.TicketQuantity -= soldTicketQuantity;

            if(@event.TicketQuantity < 1)
            {
                ViewBag.AreTicketsAvailable = false;
            }
            else
            {
                ViewBag.AreTicketsAvailable = true;
            }
            
            if (User.Identity.IsAuthenticated)//user is logged in
            {
                ViewBag.IsLoggedIn = true;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await _ticketService.ExistsByDataAsync(cancellationToken, userId, id))//ticket exists on this event for this user
                {
                    var ticket = await _ticketService.GetByDataAsync(cancellationToken, userId, id);
                    ViewBag.UserTicketExists = true;
                    ViewBag.IsBought=ticket.IsBought;
                }
                else
                {
                    ViewBag.UserTicketExists = false;
                    ViewBag.IsBought = false;
                }
            }
            else
            {
                ViewBag.IsLoggedIn = false;
                ViewBag.UserTicketExists = false;
                ViewBag.IsBought = false;
            }
           

            
            return View(@event);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventRequestModel model, CancellationToken cancellationtoken = default)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _eventService.AddAsync(cancellationtoken, model, userId);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken=default)
        {
            var @event = await _eventService.GetByIdAsync(cancellationToken, id);

            return View(@event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventRequestModel model, int id, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _eventService.UpdateAsync(cancellationToken, model, id, userId);
                await _eventService.SetIsActiveStatusAsync(cancellationToken, id, false);
                return RedirectToAction("Index");
            }
            return View(new { id, cancellationToken });
          
        }

        public async Task<IActionResult>Delete(int id, CancellationToken cancellationToken = default)
        {
            await _eventService.RemoveAsync(cancellationToken, id);
            return RedirectToAction("ArchivedEvents");
        }

        [Authorize(Roles ="Admin, Moderator")]
        public async Task<IActionResult> PendingEvents(CancellationToken cancellationToken=default)
        {
            var events = await _eventService.GetAllPendingAsync(cancellationToken);

            return View(events);
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult>Approve(int id, CancellationToken cancellationToken = default)
        {
            await _eventService.SetIsActiveStatusAsync(cancellationToken, id, true);
            return RedirectToAction("PendingEvents");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchivedEvents(CancellationToken cancellationToken = default)
        {
            var events = await _eventService.GetAllArchivedAsync(cancellationToken);

            return View(events);
        }

    }
}
