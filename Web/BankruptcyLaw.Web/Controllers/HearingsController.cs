namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BankruptcyLaw.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class HearingsController : BaseController
    {
        private IHearingsService hearingsService;

        public HearingsController(IHearingsService hearingsService)
        {
            this.hearingsService = hearingsService;
        }

        public IActionResult Create()
        {

            return this.View();
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
