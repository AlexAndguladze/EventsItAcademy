using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Persistence.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScorpe = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScorpe.ServiceProvider.GetService<EventsItAcademyDbContext>();
                context.Database.EnsureCreated();

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole>()
                    {
                        new IdentityRole(){Name = "Admin", NormalizedName ="ADMIN", ConcurrencyStamp="Admin"},
                        new IdentityRole(){Name = "Moderator", NormalizedName ="MODERATOR", ConcurrencyStamp="Moderator"},
                        new IdentityRole(){Name = "User", NormalizedName ="USER", ConcurrencyStamp="User"}
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
