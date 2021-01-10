namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Documents;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ClientDocumentsController : BaseController
    {
        private IClientDocumentsService clientDocumentsService;
        private IWebHostEnvironment environment;

        public ClientDocumentsController(IClientDocumentsService clientDocumentsService, IWebHostEnvironment environment)
        {
            this.clientDocumentsService = clientDocumentsService;
            this.environment = environment;
        }

        public IActionResult All(string caseId)
        {
            var model = this.clientDocumentsService.GetAllClientDocumentsForCase(caseId);

            return this.View(model);
        }

        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadClientDocumentViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.clientDocumentsService.CreateAsync(input, userId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View();
            }

            return this.RedirectToAction("All", new { caseId = input.CaseId });
        }
    }
}
