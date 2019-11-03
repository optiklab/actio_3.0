using Actio.Common.Auth;
using Actio.Common.Commands;
using Actio.Common.Mongo;
using Actio.Common.RabbitMq;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Handlers;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Identity
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

            services.AddJwt(Configuration);

            // Our custom configuration of RMQ.
            services.AddRabbitMq(Configuration);

            // Link handlers interfaces with handlers.
            services.AddSingleton<ICommandHandler<CreateUser>, CreateUserHandler>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<IEncrypter, Encrypter>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<IUserRepository, UserRepository>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.

            services.AddSingleton<IUserService, UserService>(); // Changed from Scoped to Singleton. Looks like .NET Core 2.2 changes.
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
