using Actio.Common.Commands;
using Actio.Common.Mongo;
using Actio.Common.RabbitMq;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Handlers;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<string>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddMongoDb(Configuration);

            // Our custom configuration of RMQ.
            services.AddRabbitMq(Configuration);

            // Link handlers interfaces with handlers.
            services.AddSingleton<ICommandHandler<CreateActivity>, CreateActivityHandler>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<ICategoryRepository, CategoryRepository>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.
            services.AddSingleton<IActivityRepository, ActivityRepository>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<IDatabaseSeeder, CustomMongoSeeder>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<IActivityService, ActivityService>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-2.2
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseMvc();
        }
    }
}
