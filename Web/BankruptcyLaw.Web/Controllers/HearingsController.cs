namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IActionResult Create(string caseId)
        {

            return this.View();
        }

        public IActionResult CaseHearings(string caseId)
        {
            var model = this.hearingsService.GetHearingsForCaseId(caseId);

            return this.View(model);
        }

        public IActionResult AttorneyHearings()
        {

            return this.View();
        }
    }
}
