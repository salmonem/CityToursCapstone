using System;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FinalCapstone.Areas.Identity.IdentityHostingStartup))]
namespace FinalCapstone.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FinalCapstoneContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CityToursDb")));

                services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<FinalCapstoneContext>();
            });
        }
    }
}