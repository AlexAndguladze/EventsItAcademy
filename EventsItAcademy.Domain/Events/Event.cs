using EventsItAcademy.Domain.Abstractions;
using EventsItAcademy.Domain.Images;
using EventsItAcademy.Domain.Tickets;
using EventsItAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Domain.Events
{
    public class Event:BaseEntity
    {
        public string Title { get; set; }
        public string Destription { get; set; }
        public int TicketQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public string OwnerId { get; set; }
        public int? ImageId { get; set; }

        public User User { get; set; }
        public Image Image { get; set; }
        public List<Ticket> Tickets { get; set; }
        
    }
}
