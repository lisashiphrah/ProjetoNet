using System;
using System.Text;
using AutoMapper;
using Eventos.IO.Infra.CrossCutting.Bus;
using Eventos.IO.Infra.CrossCutting.Identity.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Eventos.IO.Infra.CrossCutting.Identity.Data;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Eventos.IO.Infra.CrossCutting.IoC;
using Eventos.IO.Services.Api.Configurations;
using Eventos.IO.Services.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using Eventos.IO.Infra.CrossCutting.Identity.Services;
using Eventos.IO.Application.Interfaces;
using Eventos.IO.Application.Services;

namespace Eventos.IO.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string SecretKey = "DublinEvents@myToken";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                /*options =>
                {
                    options.Cookies.ApplicationCookie.AutomaticChallenge = false;
                }*/)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IEventoAppService, EventoAppService>();

            services.AddOptions();
            services.AddMvc(options =>
            {
                // OutputFormatter para não aceitar Xml apenas Json.
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));

                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeLerEventos", policy => policy.RequireClaim("Eventos", "Ler"));
                options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Eventos", "Gravar"));
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
            });            

            services.Configure<JwtTokenOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAutoMapper();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Eventos.IO API",
                    Description = "API do site Eventos.IO",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Desenvolvedor X", Email = "email@eventos.io", Url = "http://eventos.io" },
                    License = new License { Name = "MIT", Url = "http://eventos.io/licensa" }
                });
            });

            // Registrar todos os DI
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                            IHostingEnvironment env,
                            ILoggerFactory loggerFactory,
                            IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Para garantir que o servico só responda a dominios conhecidos
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc();

            //app.UseSwaggerAuthorized();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventos.IO API v1.0");
            });

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
