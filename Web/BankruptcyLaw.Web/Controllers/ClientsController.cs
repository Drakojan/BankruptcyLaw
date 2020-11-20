using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankruptcyLaw.Web.Controllers
{
    public class ClientsController : BaseController
    {
        public ClientsController()
        {

        }

        [Authorize(Roles = "Attorney")]
        public IActionResult All()
        {
            //var viewModel = new CreateCaseInputViewModel
            //{
            //    Judges = this.judgesService.GetJudgesNamesAndIds(),
            //    Trustees = this.trusteeService.GetTrusteesNamesAndIds(),
            //    AttorneyId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
            //};

            return this.View();
        }
    }
}
