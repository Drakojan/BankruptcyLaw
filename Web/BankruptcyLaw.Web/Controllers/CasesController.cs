namespace BankruptcyLaw.Web.Controllers
{
    using System.Security.Claims;

    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Cases;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private readonly IJudgesService judgesService;
        private readonly ITrusteesService trusteeService;

        public CasesController(IJudgesService judgesService, ITrusteesService trusteeService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.judgesService = judgesService;
            this.trusteeService = trusteeService;
        }

        [Authorize(Roles = "Attorney")]
        public IActionResult Create()
        {
            var viewModel = new CreateCaseInputViewModel
            {
                Judges = this.judgesService.GetJudgesNamesAndIds(),
                Trustees = this.trusteeService.GetTrusteesNamesAndIds(),
                AttorneyId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),

                // this.User.FindFirst(ClaimTypes.NameIdentifier).Value
                // this.userManager.GetUserAsync(this.user).Id
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles ="Attorney")]
        public IActionResult Create(CreateCaseInputViewModel input)
        {
            return this.View(input);
        }
    }
}
