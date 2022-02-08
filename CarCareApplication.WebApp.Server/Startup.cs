using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.WebApp.Server.Utility;
using FluentScheduler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;

namespace CarCareApplication.WebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Data Source=SQL5104.site4now.net;Initial Catalog=db_a7308d_CarCareApplication;User Id=db_a7308d_CarCareApplication_admin;Password=Ahmed!994
            services.AddDbContext<CarCareApplicationDbContext>(options => options.UseMySQL("Server=sql329.main-hosting.eu;Database=u869331255_CarCareApplication;user=u869331255_AdminCarCareApplication;password=Ahmed!994"));

            services.AddTransient<UserRepo>();
            services.AddTransient<TransactionRepo>();
            services.AddTransient<AddressRepo>();
            services.AddTransient<CarTypeRepo>();
            services.AddTransient<DayRepo>();
            services.AddTransient<HourOfWorkRepo>();
            services.AddTransient<HourRepo>();
            services.AddTransient<ServiceRepo>();
            services.AddTransient<ExpenseRepo>();
            services.AddTransient<RevenueRepo>();
            services.AddTransient<SettingRepo>();
            services.AddTransient<ExtraPriceSettingRepo>();
            services.AddTransient<RoleRepo>();

            services.AddAuthorization();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<WebUtility>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false; // Only in development to disable Https 
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = Configuration["Jwt:Audience"],
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration["Jwt:Key"]))
                        };
                    });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HourOfWorkRepo hourOfWork )
        {

            JobManager.AddJob(
             async () =>
             {
                 await hourOfWork.ResetHourOfWorkAsync();
             },
             s => s.ToRunOnceAt(9, 58)
          );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
