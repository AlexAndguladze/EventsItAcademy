using EventsItAcademy.Application.Events.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventsItAcademy.API.Infrastructure.Models.ExampleModels
{
    public class EventCreateExample : IExamplesProvider<EventRequestModel>
    {
        public EventRequestModel GetExamples()
        {
            return new EventRequestModel
            {
                Title = "Some Event",
                Destription = "Some description of this wonderful event",
                TicketQuantity = 400,
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(2).AddHours(4)
            };
        }
    }
}
