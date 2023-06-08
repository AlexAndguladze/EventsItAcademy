using EventsItAcademy.Application.Tickets;
using EventsItAcademy.Application.Tickets.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsItAcademy.API.Controllers
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : ControllerBase
    {
        private ITicketService _service;
        public TicketController(ITicketService service)
        {
            _service = service;
        }
        private string GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            if (userId != null)
            {
                return userId.Value;
            }
            return string.Empty;
        }
        /// <summary>
        /// Adds new ticket in database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        ///
        [Produces("application/json")]
        [HttpPost("ticket/user/create")]
        public async Task<int> Create(CancellationToken cancellationToken, TicketRequestModel ticket)
        {
            string userId = GetUserId();
            return await _service.AddAsync(cancellationToken, ticket, userId);
        }
        /// <summary>
        /// Updates ticket in database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="ticket"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ///
        [Produces("application/json")]
        [HttpPost("ticket/user/update")]
        public async Task UpdateAsync(CancellationToken cancellationToken, TicketRequestModel ticket, int id)
        {
            string userId = GetUserId();
            await _service.UpdateAsync(cancellationToken, ticket, id, userId);
        }
    }
}
