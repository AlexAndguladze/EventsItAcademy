using EventsItAcademy.Application.Tickets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Workers.BackgroundWorkers
{
    internal class ExpiredBookedTicketWorker : BackgroundService
    {
        private readonly ILogger<ExpiredBookedTicketWorker> _logger;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IServiceProvider _serviceProvider;

        private string Schedule => "*/20 * * * * *";
        public ExpiredBookedTicketWorker(ILogger<ExpiredBookedTicketWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<ITicketService>();
                        await service.DeleteExpiredBookingsAsync(stoppingToken, 0);

                        Settings.Settings.UpdateProperties();
                    }
                    _logger.LogInformation($"ticket expiration duration is {Settings.Settings.EventBookingPeriod} days");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}
