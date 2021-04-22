using System;
using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CaptaineMoodle.Areas.Identity.IdentityHostingStartup))]
namespace CaptaineMoodle.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")),
             ServiceLifetime.Transient);

                services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<AuthDbContext>();
                
                //Policy service for require login all over the app
                /*services.AddMvc(option =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                    option.Filters.Add(new AuthorizeFilter(policy));
                }).AddXmlSerializerFormatters();*/
            });
        }
    }
}