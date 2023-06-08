using EventsItAcademy.Domain.Events;
using EventsItAcademy.Domain.Tickets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Domain.Users
{
    public class User:IdentityUser
    {
        public List<Event> Events { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
