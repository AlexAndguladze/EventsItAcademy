using EventsItAcademy.Application.Events;
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
    public class EventDateCheckWorker : BackgroundService
    {
        private readonly ILogger<EventDateCheckWorker> _logger;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IServiceProvider _serviceProvider;

        private string Schedule => "*/20 * * * * *";
        public EventDateCheckWorker(ILogger<EventDateCheckWorker> logger, IServiceProvider serviceProvider)
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
                        var service = scope.ServiceProvider.GetRequiredService<IEventService>();
                        await service.ArchiveFinishedEventsAsync(stoppingToken);
                    }
                    _logger.LogInformation(DateTime.Now.ToString("F")+"Checked for archivable events");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}
