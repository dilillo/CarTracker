using CarTracker.Domain.Aggregates;
using CarTracker.Domain.Commands;
using CarTracker.Domain.Configuration;
using CarTracker.Domain.Projectors;
using CarTracker.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarTracker.Web
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
            services
               .AddOptions<CarTrackerOptions>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.Bind(nameof(CarTrackerOptions), settings);
               });

            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider();

            services.AddSession();

            services.AddOptions();

            services.AddMediatR(typeof(AddCarCommand));

            services.AddTransient<IDomainEventRepository, DomainEventRepository>();
            services.AddTransient<IDomainViewRepository, DomainViewRepository>();
            services.AddTransient<IGetCarByIDViewProjector, GetCarByIDViewProjector>();
            services.AddTransient<IGetAllCarsViewProjector, GetAllCarsViewProjector>();
            services.AddTransient<ICarAggregate, CarAggregate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
