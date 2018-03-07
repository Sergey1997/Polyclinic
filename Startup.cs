using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Company.Data;

namespace Company
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionBuisnessLogic")));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionUsersIdentity")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
