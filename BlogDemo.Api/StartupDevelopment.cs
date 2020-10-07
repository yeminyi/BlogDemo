
using BlogDemo.Core.Interfaces;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogDemo.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BlogDemo.Infrastructure.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using BlogDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Serialization;
using System.Linq;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
        private static IConfiguration Configuration { get; set; }
        public IHostingEnvironment Environment { get; }

        public StartupDevelopment(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blog API",
                    Version = "v1"
                });
              
            });
            services.AddMvc(
              options =>
              {
                  options.ReturnHttpNotAcceptable = true;
                  // options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                  var intputFormatter = options.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                  if (intputFormatter != null)
                  {
                      intputFormatter.SupportedMediaTypes.Add("application/vnd.cgzl.post.create+json");
                      intputFormatter.SupportedMediaTypes.Add("application/vnd.cgzl.post.update+json");
                  }
                  var outputFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                  if (outputFormatter != null)
                  {
                      outputFormatter.SupportedMediaTypes.Add("application/vnd.cgzl.hateoas+json");
                  }
              })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddFluentValidation();

            services.AddDbContext<MyContext>(options =>
            {
                // var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 6001;
            });
            services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                //options.Authority = "https://localhost:5001";
                options.Authority = Configuration["IdentityServerAddress"];
                options.ApiName = "restapi";
            });
            //services.AddScoped<IPostRepository, PostRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddAutoMapper(typeof(StartupDevelopment));
            //services.AddTransient<IValidator<PostResource>, PostResourceValidator>();
            services.AddTransient<IValidator<PostAddResource>, PostAddOrUpdateResourceValidator<PostAddResource>>();
            services.AddTransient<IValidator<PostUpdateResource>, PostAddOrUpdateResourceValidator<PostUpdateResource>>();
            services.AddTransient<IValidator<PostImageResource>, PostImageResourceValidator>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddAutoMapper(typeof(StartupDevelopment));
            //services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(StartupDevelopment));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostImageRepository, PostImageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<PostPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            services.AddTransient<ITypeHelperService, TypeHelperService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevOrigin",
                    builder => builder.WithOrigins(Configuration["ClientAddress"])
                        .WithExposedHeaders("X-Pagination")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAngularDevOrigin"));
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
 
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = @"dist";
            });
        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment  env)
        {
            app.UseMyExceptionHandler(loggerFactory);
            
            app.UseCors("AllowAngularDevOrigin");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogDemo v1");
            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
               

                  spa.Options.SourcePath = @"../blog-client";


                //if (env.IsDevelopment())
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });
        }
    }
}

