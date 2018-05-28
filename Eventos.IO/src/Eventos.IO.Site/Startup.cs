using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventos.IO.Application.Interfaces;
using Eventos.IO.Application.Services;
using Microsoft.AspNetCore.Http;
using Eventos.IO.Infra.CrossCutting.Bus;
using Eventos.IO.Infra.CrossCutting.IoC;
using AutoMapper;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Infra.CrossCutting.Identity.Data;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Eventos.IO.Infra.CrossCutting.Identity.Services;

namespace Eventos.IO.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IEventoAppService, EventoAppService>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddMvc();
            services.AddAutoMapper();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                            IHostingEnvironment env,
                            IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
