using EventsItAcademy.Application.Events;
using EventsItAcademy.Application.Events.Repositories;
using EventsItAcademy.Application.Tickets;
using EventsItAcademy.Application.Tickets.Repositories;
using EventsItAcademy.Application.Users;
using EventsItAcademy.Application.Users.Repositories;
using EventsItAcademy.Domain.Users;
using EventsItAcademy.Infrastructure.Events;
using EventsItAcademy.Infrastructure.Tickets;
using EventsItAcademy.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;

namespace EventsItAcademy.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

            //added after deadline
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
        }
    }
}
