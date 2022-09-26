
using Clean.UpIndia.Data;
using Clean.UpIndia.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.UpIndia

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
          //  services.AddRazorPages();
        
        //public void ConfigureServices(IServiceCollection services)
        //{
            // NOTE: This should ALWAYS BE the FIRST service registered in the ConfigureServices() method
            // Register ApplicationDbContext as a Service that can be used using Dependency Injection (DI) in any Controller
            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    // Get the SQL Connection String from the AppSettings.json file
                    string connString = Configuration.GetConnectionString("MyDefaultConnectionString");

                    // Register EntityFramework Core Services to use SQL Server
                    options.UseSqlServer(connString);
                });


            // Register the OWIN Identity Middleware
            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //// Register the ASP.NET Razor Pages Middleware
            services
                .AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Configure the Application Cookie options
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);      // Default Session Cookie expiry is 20 minutes
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "MyAuthCookies";
                });

            //// Register the MVC Middleware 
            //// -- Needed for Swagger Documentation Middleware Service
            //// -- Needed for API Support (if applicable)
            //services
            //    .AddMvc();

            // Register the Swagger Documentation Generation Middleware Service
            //services
            //    .AddSwaggerGen(config =>
            //    {
            //        config.SwaggerDoc("v1", new OpenApiInfo
            //        {
            //            Version = "v1",
            //            Title = "My CleanUpIndia",
            //            Description = "India Moves towards Model India"
            //        });
            //    });

            //  Register the EmailSender Service to the Dependency Injection Container
             services.AddSingleton<IEmailSender, MyEmailSenderService>();       // Create once per Application Run
            // services.AddTransient<IEmailSender, MyEmailSenderService>();    // Create once per User session!
            // services.AddScoped<IEmailSender, MyEmailSenderService>();       // Create once per HTTP Request/Response Cycle

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> rolemanager,
            UserManager<IdentityUser> usermanager)
            
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
            app.UseStaticFiles();

            app.UseRouting();

            // Activate The OWIN middleware for Authentication And Authorization Services

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                // Register the endpoints for the ROUTES in the AREAS
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            ApplicationDbContextSeed.SeedIdentityRolesAsync(rolemanager).Wait();
            ApplicationDbContextSeed.SeedIdentityUserAsync(usermanager).Wait();
        }
    }
}

