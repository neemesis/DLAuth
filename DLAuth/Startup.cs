using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DLAuth.Services;

namespace DLAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentity<BL.Models.User, BL.Models.Role>()
                .AddUserManager<Infrastructure.UserManager>()
                .AddRoleManager<Infrastructure.RoleManager>()
                .AddSignInManager<Infrastructure.SignInManager>()
                .AddDefaultTokenProviders();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<DL.Interfaces.IDatabaseConnectionService>(e =>
                new DL.Concrete.DatabaseConnectionService(connectionString));
            services.AddTransient<IUserStore<BL.Models.User>, BL.Stores.UserStore>();
            services.AddTransient<IRoleStore<BL.Models.Role>, BL.Stores.RoleStore>();
            services.AddScoped<IUserClaimsPrincipalFactory<BL.Models.User>, Infrastructure.ApplicationUserClaimsPrincipalFactory>();

            FixInterfaces(services);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            services.AddAutoMapper();
        }

        private static void FixInterfaces(IServiceCollection services) {
            services.AddTransient<DL.Interfaces.IRolesRepository, DL.Repositories.RolesRepository>();
            services.AddTransient<DL.Interfaces.IUsersClaimsRepository, DL.Repositories.UsersClaimsRepository>();
            services.AddTransient<DL.Interfaces.IUsersLoginsRepository, DL.Repositories.UsersLoginsRepository>();
            services.AddTransient<DL.Interfaces.IUsersRepository, DL.Repositories.UsersRepository>();
            services.AddTransient<DL.Interfaces.IUsersRolesRepository, DL.Repositories.UsersRolesRepository>();

            services.AddTransient<BL.Interfaces.IRoleStore, BL.Stores.RoleStore>();
            services.AddTransient<BL.Interfaces.IUserStore, BL.Stores.UserStore>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
