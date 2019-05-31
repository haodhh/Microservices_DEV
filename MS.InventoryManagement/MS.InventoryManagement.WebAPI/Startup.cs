using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MS.Common.Models;
using MS.InventoryManagement.Data;
using MS.InventoryManagement.Service;
using MS.InventoryManagement.WebAPI.ActionFilters;
using MS.InventoryManagement.WebAPI.SignalRHub;
using System;
using System.Text;

namespace MS.InventoryManagement.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //string jsonFile = $"appsettings.{environment}.json";

            //var builder = new ConfigurationBuilder().AddJsonFile(jsonFile, optional: true).AddEnvironmentVariables();
            //Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageQueueAppConfig>(Configuration.GetSection("MessageQueueAppConfig"));

            CorsPolicyBuilder corsBuilder = new CorsPolicyBuilder();

            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            ConnectionStrings connectionStrings = new ConnectionStrings();
            Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

            services.AddDbContext<InventoryManagementDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<InventoryManagementDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").ToString()));

            services.AddTransient<IInventoryManagementRepository, InventoryManagementRepository>();

            services.AddTransient<IInventoryManagementService>(provider =>
            new InventoryManagementService(provider.GetRequiredService<IInventoryManagementRepository>(), connectionStrings));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://haodhh.microservices.com",
                    ValidAudience = "https://haodhh.microservices.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MS.Common.TokenManagement"))
                };
            });

            services.AddScoped<SecurityFilter>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("SiteCorsPolicy");
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageQueueHub>("/messageQueueHub");
            });
        }
    }
}