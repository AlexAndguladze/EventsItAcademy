﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Users.Requests
{
    public class UserLoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}