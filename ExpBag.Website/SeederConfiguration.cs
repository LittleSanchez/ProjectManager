using ExpBag.Domain;
using ExpBag.EFData;
using ExpBag.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website
{
    public static class SeederConfiguration
    {
        public static void ApplySeeder(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                serviceScope.ServiceProvider.GetRequiredService<DataContext>().EnsureSeeder(userManager, roleManager);
            }
        }
    }
}
