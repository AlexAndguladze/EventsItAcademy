using EventsItAcademy.Application.Users.Requests;
using EventsItAcademy.Application.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Users
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegisterRequestModel model);
        Task<UserLoginResponseModel> LoginUserAsync(UserLoginRequestModel model);
    }
}
