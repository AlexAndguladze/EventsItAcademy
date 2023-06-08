using EventsItAcademy.API.Infrastructure.Auth.JWT;
using EventsItAcademy.Application.Users;
using EventsItAcademy.Application.Users.Requests;
using EventsItAcademy.Application.Users.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EventsItAcademy.API.Controllers
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private readonly IOptions<JWTConfiguration> _options;
        public UserController(IUserService service, IOptions<JWTConfiguration> options)
        {
            _service = service;
            _options = options;
        }
        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterUserAsync(model);
                if(result==true) return Ok(result);
                else return BadRequest(result);
            }
            return BadRequest("some proeprties are not valid");
        }
        /// <summary>
        /// Logs in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///
        [HttpPost("Login")]
        public async Task<string> LoginAsync([FromBody]UserLoginRequestModel model)
        {
            var userLoginResponse = await _service.LoginUserAsync(model);

            return JWTHelper.GenerateSecurityToken(userLoginResponse.Id,userLoginResponse.Roles, _options);
        }
    }
}
