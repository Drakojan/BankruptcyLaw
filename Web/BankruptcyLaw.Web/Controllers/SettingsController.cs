namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Settings;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : BaseController
    {
        private readonly ISettingsService settingsService;

        private readonly IDeletableEntityRepository<Setting> repository;
        private readonly UserManager<IdentityUser> userManager;

        public RoleManager<ApplicationRole> roleManager { get; }

        public SettingsController(ISettingsService settingsService, IDeletableEntityRepository<Setting> repository, UserManager<IdentityUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.settingsService = settingsService;
            this.repository = repository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var settings = this.settingsService.GetAll<SettingViewModel>();
            var model = new SettingsListViewModel { Settings = settings };
            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // playing around
        public async Task<IActionResult> Test()
        {
            var result = await this.userManager.CreateAsync(
                new ApplicationUser()
                {
                    UserName = "niki",
                    Email = "niki@mail.bg",
                }, "Nekradss92");
            

            var result2 = this.roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "Attorney",
            });

            var result3 = this.roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "Client",
            });

            return this.Json(result);
        }
    }
}
