using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ServiceHost
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
            services.AddHttpContextAccessor();

            var connectionString = Configuration.GetConnectionString("LampshadeDb");

            ShopManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstrapper.Configure(services, connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);


            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                 {
                     o.LoginPath = new PathString("/Account");
                     o.LoginPath = new PathString("/Account");
                     o.AccessDeniedPath = new PathString("/AccessDenied");
                 });

            services.AddAuthorization(options =>
            {
                 options.AddPolicy("AdminArea", builder =>
                 builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentUploader }));

                options.AddPolicy("Shop", builder =>
                builder.RequireRole(new List<string> { Roles.Admin }));

                options.AddPolicy("Discount", builder =>
                builder.RequireRole(new List<string> { Roles.Admin }));

                options.AddPolicy("Account", builder =>
                builder.RequireRole(new List<string> { Roles.Admin }));

                options.AddPolicy("Inventory", builder =>
                builder.RequireRole(new List<string> { Roles.Admin }));
            });

            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminArea");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Shop", "Shop");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Accounts", "Account");
                    options.Conventions.AuthorizeAreaFolder("Admin", "/Inventory", "Inventory");
                });
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
