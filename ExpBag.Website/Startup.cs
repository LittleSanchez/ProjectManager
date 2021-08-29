using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ExpBag.EFData;
using MediatR;
using ExpBag.Website.CQRS.Auth.Login;
using ExpBag.Website.CQRS.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using ExpBag.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using ExpBag.Application.Interfaces;
using ExpBag.Infrastructure.Security;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using System.IO;
using AutoMapper;
using ExpBag.Application.AutoMapper;
using ExpBag.Domain.CQRSObjects;
using Microsoft.Extensions.FileProviders;

namespace ExpBag.Website
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
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger<Startup>();
            //services.AddSingleton(typeof(LoggerFactory), loggerFactory);
            logger.LogWarning("Log warning: {0}", "Application logger setuped correctly");
            logger.LogInformation("Log info: Working directory - {0}", Directory.GetCurrentDirectory());
            //services.AddControllersWithViews();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<DataContext>(opt =>
            {
                //opt.UseNpgsql(Configuration.GetConnectionString("TrashBagPostgresDB"));
                opt.UseSqlServer(Configuration.GetConnectionString("TrashBagDevDB"));
            });

            services.AddMediatR(typeof(LoginHandler).Assembly);
            //services.AddMediatR(typeof(RegistrationHandler).Assembly);

            // In production, the React files will be served from this directory



            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //Automapper `
            services.AddAutoMapper(MapperProfile.Create);




            services.AddTransient<IValidator<LoginQuery>, LoginQueryValidator>();
            services.AddTransient<IValidator<RegistrationCommand>, RegistrationValidation>();

            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddIdentity<AppUser, AppRole>(options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food.WebApi", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer",
            //        new OpenApiSecurityScheme
            //        {
            //            Description = "JWT Authorization header using the Bearer scheme.",
            //            Type = SecuritySchemeType.Http,
            //            Scheme = "bearer"
            //        });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
            //        {
            //            new OpenApiSecurityScheme{
            //                Reference = new OpenApiReference{
            //                    Id = "Bearer",
            //                    Type = ReferenceType.SecurityScheme
            //                }
            //            },new List<string>()
            //        }
            //    });
            //});

            services.AddScoped<IJwtGenerator, JwtGenerator>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            foreach (var staticFolders in Configuration.GetSection("StaticFolders").GetChildren())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, staticFolders["Path"])),
                    RequestPath = staticFolders["Link"]
                });

            }

            //Presets folder

            //Assets folder

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
