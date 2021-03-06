﻿namespace BankruptcyLaw.Web
{
    using System.Reflection;

    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Common;
    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Repositories;
    using BankruptcyLaw.Data.Seeding;
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Services.Messaging;
    using BankruptcyLaw.Web.Hubs;
    using BankruptcyLaw.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddUserManager<UserManager<ApplicationUser>>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddSignalR();
            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // CSRF protection
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IJudgesService, JudgesService>();
            services.AddTransient<ICasesService, CasesService>();
            services.AddTransient<ITrusteesService, TrusteesService>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddTransient<IClientDocumentsService, ClientDocumentsService>();
            services.AddTransient<IHearingsService, HearingsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/Chat");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
