using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenGameMusic.Areas.Identity.Data;
using OpenGameMusic.Data;

[assembly: HostingStartup(typeof(OpenGameMusic.Areas.Identity.IdentityHostingStartup))]
namespace OpenGameMusic.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OpenGameMusicDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OpenGameMusicDbContextConnection")));

                services.AddDefaultIdentity<OpenGameMusicUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    }) // we dont use gmail confirmation
                    .AddEntityFrameworkStores<OpenGameMusicDbContext>();
            });
        }
    }
}