using AppConfigurator.Library.Application;
using AppConfigurator.Library.Interfaces;
using AppConfigurator.Library.Utils;
using AppConfigurator.Service.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DATA = AppConfigurator.Data.DataProvider;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace AppConfigurator
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new ApplicationModelConverter());
                })
                .AddControllersAsServices();

            //built-in DI
            services.AddScoped<IApplicationModel, ApplicationModel>();
            services.AddSingleton<IRedisManager>(new RedisManager(Configuration.GetConnectionString("RedisConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var redisManager = app.ApplicationServices.GetService<IRedisManager>();
            redisManager.AddOrUpdateAsync(DATA.Database.GetConfigurations());


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}