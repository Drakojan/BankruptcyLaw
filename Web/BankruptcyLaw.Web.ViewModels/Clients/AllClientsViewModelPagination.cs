namespace BankruptcyLaw.Web.ViewModels.Clients
{
    using System.Collections.Generic;

    public class AllClientsViewModelPagination : PagingViewModel
    {
        public IEnumerable<SingleClientViewModel> Clients { get; set; }
    }
}
