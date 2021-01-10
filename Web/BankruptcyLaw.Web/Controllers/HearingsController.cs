namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Hearings;
    using Microsoft.AspNetCore.Mvc;

    public class HearingsController : BaseController
    {
        private IHearingsService hearingsService;

        public HearingsController(IHearingsService hearingsService)
        {
            this.hearingsService = hearingsService;
        }

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

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHearingViewModel input)
        {
            var attorneyId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.AttorneyId = attorneyId;

            try
            {
                await this.hearingsService.CreateAsync(input);
            }
            catch (ArgumentNullException ex)
            {
                return this.RedirectToAction("Create", new { message = ex.Message });
            }

            return this.RedirectToAction("Create", new { message = "Hearing Successfully Added" });
        }

        public IActionResult CaseHearings(string caseId)
        {
            var model = this.hearingsService.GetHearingsForCaseId(caseId);

            return this.View(model);
        }

        public IActionResult AttorneyAllHearings()
        {
            var attorneyId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = this.hearingsService.GetHearingsForAttorneyId(attorneyId);

            return this.View(model);
        }
    }
}
