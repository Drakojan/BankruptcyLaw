namespace BankruptcyLaw.Web.Controllers
{
    using System.Diagnostics;

    using BankruptcyLaw.Common;
    using BankruptcyLaw.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ErrorController : BaseController
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("/Error/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    this.ViewData["ErrorMessage"] = GlobalConstants.NotFoundMessage;
                    return this.View("NotFound");
                case 401:
                    this.ViewData["ErrorMessage"] = GlobalConstants.UnauthorizedMessage;
                    return this.View("Unauthorized");
                case 403:
                    this.ViewData["ErrorMessage"] = GlobalConstants.UnauthorizedMessage;
                    return this.View("Unauthorized");

                default: return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }
        }
    }
}
