using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Tickets.Responses
{
    public class TicketResponseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public bool IsBought { get; set; }
    }
}
