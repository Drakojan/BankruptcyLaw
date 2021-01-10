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
        private readonly UserManager<ApplicationUser> userManager;

        public SettingsController(ISettingsService settingsService, IDeletableEntityRepository<Setting> repository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.settingsService = settingsService;
            this.repository = repository;
            this.userManager = userManager;
            this.RoleManager = roleManager;
        }

        public RoleManager<ApplicationRole> RoleManager { get; }

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
        public async Task<IActionResult> Attorney()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.userManager.AddToRoleAsync(user, "Attorney");

            // await this.userManager.RemoveFromRoleAsync(user, "Client");
            return this.Json(result);
        }

        public async Task<IActionResult> Admin()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.userManager.AddToRoleAsync(user, "Administrator");
            return this.Json(result);
        }
    }
}
