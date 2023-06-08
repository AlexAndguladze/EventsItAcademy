using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Tickets.Requests
{
    public class TicketRequestModel
    {
       // public string UserId { get; set; }
        public int EventId { get; set; }
        public bool IsBought { get; set; }
    }
}
