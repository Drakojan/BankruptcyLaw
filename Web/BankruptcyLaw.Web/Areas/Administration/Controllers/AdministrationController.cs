namespace BankruptcyLaw.Web.Areas.Administration.Controllers
{
    using BankruptcyLaw.Common;
    using BankruptcyLaw.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
