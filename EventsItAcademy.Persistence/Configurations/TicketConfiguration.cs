using EventsItAcademy.Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Persistence.Configurations
{
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(t => t.User).WithMany(u => u.Tickets).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t=>t.Event).WithMany(e=>e.Tickets).HasForeignKey(t=>t.EventId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
