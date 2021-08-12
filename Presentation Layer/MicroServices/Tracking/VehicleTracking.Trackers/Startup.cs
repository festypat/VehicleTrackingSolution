using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using VehicleTracking.ApplicationCore.Trackers.Interfaces.Service;
using VehicleTracking.ApplicationCore.Trackers.Services;
using VehicleTracking.BackgroundService.Tracking.BackgroundServices;
using VehicleTracking.BackgroundService.Tracking.Interfaces;
using VehicleTracking.BackgroundService.Tracking.Repository;
using VehicleTracking.BackgroundService.Tracking.Service;
using VehicleTracking.BackgroundService.Tracking.TaskSchedules;
using VehicleTracking.Infrastructure.Tracking.Interfaces;
using VehicleTracking.Infrastructure.Tracking.Services;
using VehicleTracking.Tracking.Helper.Extensions.Utilities;
using VehicleTracking.Tracking.Helper.Notification;
using VehicleTracking.Tracking.Persistance.Context;
using VehicleTracking.Tracking.Persistance.Repositories;

namespace VehicleTracking.Trackers
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleTracking.Trackers", Version = "v1" });
            });

            var geolocationSection = Configuration.GetSection(nameof(GeoLocationConfigurations));
            services.Configure<GeoLocationConfigurations>(geolocationSection);

            var identityConfig = Configuration.GetSection(nameof(IdentityConfiguration));
            services.Configure<IdentityConfiguration>(identityConfig);

            var appSettings = identityConfig.Get<IdentityConfiguration>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });


            services.AddOptions();
            services.AddScoped<ITrackingService, TrackingService>();
            services.AddScoped<IGeoLocation, GeoLocationService>();

            services.AddDbContext<TrackingDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TrackingDbContextString")));

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ITrackingHistoryService, TrackingHistoryService>();
            services.AddScoped<LocationRepository>();
            services.AddScoped<INotificationTask, NotificationTask>();
            services.AddScoped<TrackingLocationRepository>();

            var options = Configuration.GetSection(nameof(CronExpressions)).Get<CronExpressions>();

            services.AddCronJob<LocationTask>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = options.LocationTask;
            });

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleTracking.Trackers v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
