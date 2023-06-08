using EventsItAcademy.Application.CustomExcepitons;
using EventsItAcademy.Application.Localization;
using EventsItAcademy.Application.Users.Requests;
using EventsItAcademy.Application.Users.Responses;
using EventsItAcademy.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Users
{
    public class UserService: IUserService
    {
        private UserManager<User> _userManager;
 

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserLoginResponseModel> LoginUserAsync(UserLoginRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user == null)
            {
                throw new InvalidEmailException(ErrorMessages.InvalidEmail);
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.CheckPasswordAsync(user, model.Password); 
            if(result == false)
            {
                throw new InvalidPasswordException(ErrorMessages.InvalidPassword);
            }

            var loginResponse = new UserLoginResponseModel()
            {
                Id = user.Id,
                Roles = roles.ToList()
            };

            return loginResponse;
            
        }

        public async Task<bool> RegisterUserAsync(UserRegisterRequestModel model)
        {
            if(model == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            if(model.Password != model.ConfirmPassword)
            {
                throw new Exception("Password doesn't match");
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Registration failed");
            }

        }
    }
}
