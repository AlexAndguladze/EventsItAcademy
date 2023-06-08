using EventsItAcademy.Application.Tickets;
using EventsItAcademy.Application.Tickets.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventsItAcademy.WEB.Controllers
{
    public class TicketController : Controller
    {

        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        [Authorize]
        public async Task<IActionResult> Buy(int eventId, CancellationToken cancellationToken = default)
        { 
            var ticket = new TicketRequestModel()
            {
                EventId = eventId,
                IsBought = true
            };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.AddAsync(cancellationToken, ticket, userId);

            return RedirectToAction("Details", "Event", new { id = eventId });
        }
        [Authorize]
        public async Task<IActionResult> Book(int eventId, CancellationToken cancellationToken = default)
        {
            var ticket = new TicketRequestModel()
            {
                EventId = eventId,
                IsBought = false
            };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.AddAsync(cancellationToken, ticket, userId);

            return RedirectToAction("Details", "Event", new { id = eventId });
        }

        [Authorize]
        public async Task<IActionResult> BuyBooked(int eventId, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _service.ExistsByDataAsync(cancellationToken, userId, eventId))
            {
                var ticketId = await _service.GetIdByDataAsync(cancellationToken, userId, eventId);
                var ticketRequestModel = new TicketRequestModel()
                {
                    EventId = eventId,
                    IsBought = true
                };
                await _service.UpdateAsync(cancellationToken, ticketRequestModel, ticketId, userId);
            }
            return RedirectToAction("Details", "Event", new { id = eventId });
        }
    }
}
