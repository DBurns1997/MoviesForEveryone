using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesForEveryone.Areas.Identity.Data;
using MoviesForEveryone.Data;

[assembly: HostingStartup(typeof(MoviesForEveryone.Areas.Identity.IdentityHostingStartup))]
namespace MoviesForEveryone.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MoviesForEveryoneContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MoviesForEveryoneContextConnection")));

                services.AddDefaultIdentity<WebsiteUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MoviesForEveryoneContext>();
            });
        }
    }
}