namespace BankruptcyLaw.Web.Controllers
{
    using BankruptcyLaw.Web.ViewModels.Cases;
    using Microsoft.AspNetCore.Mvc;

    public class CasesController : BaseController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Create(CreateCaseInputViewModel input)
        {
            return this.View();
        }
    }
}
