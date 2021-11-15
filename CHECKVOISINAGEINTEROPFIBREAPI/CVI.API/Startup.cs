using CVI.Infrastructure;
using AutoMapper;
using CVI.Service.CVI;
using CVI.Service.CVI.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CVI.Service.AutoMapper;
using CVI.Service.Referential.Contract;
using CVI.Service.Referential;
using CVI.Service.Authent.Contract;
using CVI.Service.Authent;
using Microsoft.AspNetCore.Http;
using CVI.Service.AlarmSystem.Contract;
using CVI.Service.AlarmSystem;
using CVI.Domain.Common;
using System;
using System.IO;

namespace CVI.API
{
    /// <summary>
    /// The Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Get The Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = (IConfiguration)new ConfigurationBuilder()
          .SetBasePath(AppContext.BaseDirectory)
          .AddJsonFile("appsettings.json", false, true)
          .AddJsonFile("appsettings." + environment.EnvironmentName + ".json", true, true)
          .Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Adds a default in-memory implementation of IDistributedCache
            services.AddDistributedMemoryCache(); 
            
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            var connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            services.AddDbContext<CviContext>(
                options => options.UseSqlServer(connectionString)
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();

            services.AddAuthorization();

            //Injections
            services.AddTransient<ICviService, CviService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IReferentialService, ReferentialService>();
            services.AddTransient<IAlarmSystemService, AlarmSystemService>();
            
                
            services.AddOptions();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CVI API",
                    Description = "CVI API WEB SERVICE ASP CORE 3.1",
                    Contact = new OpenApiContact
                    {
                        Email = "YourTeamMailAdress@yourCompanyDomain.fr",
                        Name = "FIT_DEV"
                    }
                });
            });

            #region AutoMapper

            // AutoMapper Configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CviContext>();
                //create DB 
                context.Database.Migrate();

                //Seed data : 
                try
                {
                    var script = File.ReadAllText("./Data/seedData.sql");
                    context.Database.ExecuteSqlRaw(script);
                }
                catch { 
                    //
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();


            app.UseCors(
                options => options.WithOrigins("http://localhost:8080",
                    "http://localhost:44323").WithExposedHeaders("content-disposition")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            loggerFactory.AddLog4Net();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "CVI API V1");
                options.OAuthAppName("CVI Swagger UI");
            });

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async context => {
                context.Response.Redirect("/swagger");
            });
        }
    }
}
