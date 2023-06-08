
using EventsItAcademy.API.Infrastructure.Models.ExampleModels;
using EventsItAcademy.Application.Events;
using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Events.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EventsItAcademy.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventController : ControllerBase
    {
        private IEventService _service;
        public EventController(IEventService service)
        {
            _service = service;
        }
        [HttpGet("get-my-id")]
        private string GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            if (userId != null)
            {
                return userId.Value;
            }
            return string.Empty;
        }
        [HttpGet("get-my-roles")]
        private string GetUserRoles()
        {
            var userRoles = User.Claims.FirstOrDefault(x => x.Type.Equals("UserRoles", StringComparison.InvariantCultureIgnoreCase));
            if (userRoles != null)
            {
                return userRoles.Value;
            }
            return string.Empty;
        }
        /// <summary>
        /// Gets specified Event from database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ///
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        [HttpGet("v1/user/{id}")]
        
        public  async Task<EventResponseModel> GetbyId(CancellationToken cancellationToken, int id)
        {
            string userId = GetUserId();
            return await _service.GetByIdAsync(cancellationToken, id, userId);
        }
        /// <summary>
        /// Gets authorized User's Events from database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ///
        [HttpGet("user")]
        public async Task<List<EventResponseModel>>GetAllOfUser(CancellationToken cancellationToken)
        {
            string userId = GetUserId();
            return await _service.GetAllofUserAsync(cancellationToken, userId);
        }
        /// <summary>
        /// Gets all events from database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ///
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<EventResponseModel>>GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
        /// <summary>
        /// Adds new event in database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        ///
        [SwaggerRequestExample(typeof(EventRequestModel), typeof(EventCreateExample))]
        [HttpPost("user")]
        public async Task<int> Create(CancellationToken cancellationToken, EventRequestModel @event)
        {
            string userId = GetUserId();
            return await _service.AddAsync(cancellationToken, @event, userId);
        }

        /// <summary>
        /// Deletes specified event from database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _service.RemoveAsync(cancellationToken, id);
        }
        
    }
}
