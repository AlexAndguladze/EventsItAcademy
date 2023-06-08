// See https://aka.ms/new-console-template for more information
using EventsItAcademy.Application.Events;
using EventsItAcademy.Application.Events.Repositories;
using EventsItAcademy.Application.Tickets;
using EventsItAcademy.Application.Tickets.Repositories;
using EventsItAcademy.Infrastructure.Events;
using EventsItAcademy.Infrastructure.Tickets;
using EventsItAcademy.Persistence.Data;
using EventsItAcademy.Workers.BackgroundWorkers;
using EventsItAcademy.Workers.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    await CreateHostBuilder(args).Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}
static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<EventsItAcademyDbContext>(options =>
        options.UseSqlServer(hostContext.Configuration.GetConnectionString("EventsItAcademyDbContextConnection") 
        ?? throw new InvalidOperationException("Connection string 'EventsItAcademyDbContextConnection' not found.")
        ));
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddHostedService<ExpiredBookedTicketWorker>();
        services.AddHostedService<EventDateCheckWorker>();

    }).UseSerilog();
