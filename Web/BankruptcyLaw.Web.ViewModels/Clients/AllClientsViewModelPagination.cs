using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptcyLaw.Web.ViewModels.Clients
{
    public class AllClientsViewModelPagination
    {
        public int CurrentPage { get; set; }

        public IEnumerable<SingleClientViewModel> Clients { get; set; }

    }
}
