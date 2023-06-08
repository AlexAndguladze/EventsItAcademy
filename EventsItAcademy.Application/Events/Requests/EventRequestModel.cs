using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Events.Requests
{
    public class EventRequestModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Destription { get; set; }

        [Range(0.0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int TicketQuantity { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
