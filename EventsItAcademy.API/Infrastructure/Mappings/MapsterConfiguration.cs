using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Events.Responses;
using EventsItAcademy.Application.Tickets.Requests;
using EventsItAcademy.Application.Tickets.Responses;
using EventsItAcademy.Domain.Events;
using EventsItAcademy.Domain.Tickets;
using Mapster;

namespace EventsItAcademy.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            TypeAdapterConfig<EventRequestModel, Event>.NewConfig();
            TypeAdapterConfig<Event, EventResponseModel>.NewConfig();

            TypeAdapterConfig<TicketRequestModel, Ticket>.NewConfig();
            TypeAdapterConfig<Ticket, TicketResponseModel>.NewConfig();
        }
    }
}
