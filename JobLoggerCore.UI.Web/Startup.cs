using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
 

using JobLoggerCORE.IRepositories;
using JobLoggerCORE.Repositories;
using JobLoggerCORE.IServices;
using JobLoggerCORE.Services;

namespace JobLoggerCore.UI.Web
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
            services.AddMvc();
            RegisterServices(services);
         //   services.AddDbContext<JobLoggerCORE.Data.JobLoggerContext>(options =>
         //options.UseSqlServer(Configuration.GetConnectionString("JobLoggerEntities")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public  IServiceCollection RegisterServices(
           IServiceCollection services)
        {
           Dictionary<Type,Type> dependencies = JobLoggerCORE.DependencyInjection.DependencyResolver.GetAssembliesDependencies("JobLoggerCORE.");
            foreach (var item in dependencies)
            {
                services.AddTransient(item.Key, item.Value);
            }
            //services.AddTransient<ILogConfigurationRepository,LogConfigurationRepository>();
            //services.AddTransient<ILogMessageRepository, LogMessageRepository>();
            //services.AddTransient<ITypeRepository, TypeRepository>();
            //services.AddTransient<ILogConfigurationService, LogConfigurationService>();
            //services.AddTransient<ILogMessageService, LogMessageService>();
            //services.AddTransient<ITypeService, TypeService>();            
            // Add all other services here.
            return services;
        }

    }
}
