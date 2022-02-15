using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.MongoDB;
using OnionAppTraining.Infrastructure.Repositories;
using OnionAppTraining.Infrastructure.Services;
using OnionAppTraining.Infrastructure.Settings;
using System;
using System.Linq;

namespace OnionAppTraining.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
            services.Configure<GeneralSettings>(Configuration.GetSection(nameof(GeneralSettings)));
            services.AddSingleton<IMongoDBSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddAutoMapper(x => x.AddProfile(new BaseMapperProfile()));
            services.AddTransient<ICommandDispatcher, CommandDispatcher>()
                .AddTransient<IDriverRepository, InMemoryDriverRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IDriverService, DriverService>()
                .AddTransient<IHandler, Handler>()
                .AddTransient<IRouteManager, RouteManager>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IVehicleProvider, VehicleProvider>()
                .AddSingleton<IEncrypter, Encrypter>()
                .AddSingleton<IJwtHandler, JwtHandler>()
                .AddSingleton<IDataInitializer, DataInitializer>()
                .AddSingleton<IGeneralSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<GeneralSettings>>().Value);
            services.AddControllers();
            services.AddRazorPages();
            services.AddMemoryCache();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));

            var commandHandlers = typeof(Startup).Assembly.GetTypes()
             .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (var handler in commandHandlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)), handler);
            }

            var jwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.Key)),
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = tokenValidationParameters;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            var generalSettings = app.ApplicationServices.GetService<IGeneralSettings>();
            if (generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
            MongoConfigurator.Initialize();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
