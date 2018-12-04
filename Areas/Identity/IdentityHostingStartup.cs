using System;
using EVE_Moon_Map.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EVE_Moon_Map.Areas.Identity.IdentityHostingStartup))]
namespace EVE_Moon_Map.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EVE_Moon_MapContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EVE_Moon_MapContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<EVE_Moon_MapContext>();
            });
        }
    }
}