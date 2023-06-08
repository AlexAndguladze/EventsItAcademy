using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Events.Responses
{
    public class EventResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Destription { get; set; }
        public int TicketQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string OwnerId { get; set; }
    }
}
