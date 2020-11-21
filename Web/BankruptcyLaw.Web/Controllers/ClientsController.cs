namespace BankruptcyLaw.Web.Controllers
{
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Clients;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClientsController : BaseController
    {
        private IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [Authorize(Roles = "Attorney")]
        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 2;

            var viewModel = new AllClientsViewModelPagination()
            {
                Clients = this.clientsService.GetAll<SingleClientViewModel>(id, itemsPerPage),
                CurrentPageNumber = id,
                ItemsPerPage = itemsPerPage,
                ClientsTotalCount = this.clientsService.GetClientsTotal(),
            };

            return this.View(viewModel);
        }
    }
}
