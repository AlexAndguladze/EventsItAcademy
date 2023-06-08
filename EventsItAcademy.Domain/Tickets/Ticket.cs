using EventsItAcademy.Domain.Abstractions;
using EventsItAcademy.Domain.Events;
using EventsItAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Domain.Tickets
{
    public class Ticket:BaseEntity
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
        public bool IsBought { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }

    }
}
