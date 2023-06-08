using EventsItAcademy.Domain.Abstractions;
using EventsItAcademy.Domain.Events;
using EventsItAcademy.Domain.Images;
using EventsItAcademy.Domain.Tickets;
using EventsItAcademy.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsItAcademy.Persistence.Data
{
    public class EventsItAcademyDbContext:IdentityDbContext<User>
    {
        public EventsItAcademyDbContext(DbContextOptions<EventsItAcademyDbContext> options):base(options)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseEntity && (
              e.State == EntityState.Added
              || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).UpdatedOn = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedOn = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(EventsItAcademyDbContext).Assembly);
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
