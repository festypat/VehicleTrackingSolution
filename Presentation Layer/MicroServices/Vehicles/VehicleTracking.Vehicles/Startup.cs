using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using VehicleTracking.Vehicle.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Infra.Ioc.Vehicle.Container;

namespace VehicleTracking.Vehicles
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleTracking.Vehicles", Version = "v1" });
            });

           // services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddOptions();
            //services.AddScoped<IVehicleService, VehicleService>();

            //services.AddTransient<IEventBus, EventMQBus>();

            //services.AddTransient<ISendMessageService, SendMessageService>();

            //services.AddTransient<IRequestHandler<CreateNewVehicleCommand, bool>, VehicleHandler>();

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));

            services.AddDbContext<VehicleDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("VehicleDbContextString")));

            ////var options = new RabbitMQOptions();
            ////Configuration.GetSection(nameof(RabbitMQOptions)).Bind(options);
            ////services.Configure<RabbitMQOptions>(Configuration.GetSection(nameof(RabbitMQOptions)));

            ////services.AddRawRabbit(new RawRabbitOptions
            ////{
            ////    ClientConfiguration = options
            ////});

            ////services.AddSingleton<IEventListener, RabbitMQListener>();
            services.AddMediatR(typeof(Startup));
            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleTracking.Vehicles v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }
}
