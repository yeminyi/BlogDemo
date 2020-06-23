
using BlogDemo.Core.Interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogDemo.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BlogDemo.Infrastructure.Resources;
using FluentValidation;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
        private static IConfiguration Configuration { get; set; }

        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MyContext>(options =>
            {
                // var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(StartupDevelopment));
            services.AddTransient<IValidator<PostResource>, PostResourceValidator>();

        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMyExceptionHandler(loggerFactory);

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}

