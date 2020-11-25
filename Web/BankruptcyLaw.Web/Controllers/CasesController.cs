namespace BankruptcyLaw.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels;
    using BankruptcyLaw.Web.ViewModels.Cases;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private readonly IJudgesService judgesService;
        private readonly ITrusteesService trusteeService;
        private readonly ICasesService casesService;

        public CasesController(IJudgesService judgesService, ITrusteesService trusteeService, UserManager<ApplicationUser> userManager, ICasesService casesService)
        {
            this.userManager = userManager;
            this.judgesService = judgesService;
            this.trusteeService = trusteeService;
            this.casesService = casesService;
        }

        [Authorize(Roles = "Attorney, Administrator")]
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
        [Authorize(Roles = "Attorney, Administrator")]
        public async Task<IActionResult> Create(string clientId, CreateCaseInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Error", new ErrorViewModel() { RequestId = null });
            }

            await this.casesService.CreateCaseAsync(clientId, input);

            return this.Json(input);
        }

        [Authorize]
        public IActionResult AllClientCases(string clientName, string clientId)
        {
            AllClientCasesViewModel model = this.casesService.GetAllCasesForClient(clientId, clientName);

            return this.View(model);
        }
    }
}
