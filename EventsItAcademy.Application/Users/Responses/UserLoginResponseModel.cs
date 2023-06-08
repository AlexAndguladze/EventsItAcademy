using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Users.Responses
{
    public class UserLoginResponseModel
    {
        public string Id { get; set; }
        public List<string> Roles { get; set; }
    }
}
