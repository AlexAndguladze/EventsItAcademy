using EventsItAcademy.Domain.Abstractions;
using EventsItAcademy.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Domain.Images
{
    public class Image:BaseEntity
    {
        public string Source { get; set; }
        public List<Event> Events { get; set; }
    }
}
