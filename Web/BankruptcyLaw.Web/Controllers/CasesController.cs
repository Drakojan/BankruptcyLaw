namespace BankruptcyLaw.Web.Controllers
{
    using System;
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
        private readonly IJudgesService judgesService;
        private readonly ITrusteesService trusteeService;
        private readonly ICasesService casesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CasesController(IJudgesService judgesService, ITrusteesService trusteeService, UserManager<ApplicationUser> userManager, ICasesService casesService)
        {
            this.userManager = userManager;
            this.judgesService = judgesService;
            this.trusteeService = trusteeService;
            this.casesService = casesService;
        }

        [Authorize(Roles = "Attorney, Administrator")]
        public IActionResult Create(string message)
        {
            if (message != null)
            {
                this.ViewData["message"] = message;
            }
            else
            {
                this.ViewData["message"] = null;
            }

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
                // TODO: see why validation is not working
                return this.View("Error", new ErrorViewModel() { RequestId = null });
            }

            string caseId;

            try
            {
                caseId = await this.casesService.CreateCaseAsync(clientId, input);
            }
            catch (ArgumentException ex)
            {
                return this.RedirectToAction("Create", new { message = ex.Message });
            }

            return this.RedirectToAction("CaseDetails", new { clientId, caseId });
        }

        [Authorize(Roles = "Attorney, Administrator")]
        public async Task<IActionResult> Delete(string caseId, string clientName, string clientId)
        {
            await this.casesService.DeleteCaseByIdAsync(caseId);

            return this.RedirectToAction("AllClientCases", new { clientName, clientId });
        }

        public IActionResult CaseDetails(string caseId)
        {
            var model = this.casesService.GetCaseById(caseId);

            return this.View(model);
        }

        [Authorize]
        public IActionResult AllClientCases(string clientName, string clientId)
        {
            AllClientCasesViewModel model = this.casesService.GetAllCasesForClient(clientId, clientName);

            return this.View(model);
        }
    }
}
