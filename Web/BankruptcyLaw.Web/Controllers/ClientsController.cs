using BankruptcyLaw.Services.Data;
using BankruptcyLaw.Web.ViewModels.Clients;
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
        private IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [Authorize(Roles = "Attorney")]
        public IActionResult All(int id = 1)
        {
            var viewModel = new AllClientsViewModelPagination()
            {
                Clients = this.clientsService.GetAll<SingleClientViewModel>(id, 5),
                CurrentPage = id,
            };

            return this.View(viewModel);
        }
    }
}
