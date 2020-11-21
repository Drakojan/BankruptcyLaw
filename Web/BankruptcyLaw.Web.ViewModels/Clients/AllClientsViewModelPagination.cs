namespace BankruptcyLaw.Web.ViewModels.Clients
{
    using System;
    using System.Collections.Generic;

    public class AllClientsViewModelPagination : PagingViewModel
    {
        // public int CurrentPageNumber { get; set; }

        //public bool HasPreviousPage => this.CurrentPageNumber > 1;

        //public bool HasNextPage => this.CurrentPageNumber < this.PagesCount;

        //public int PreviousPageNumber => this.CurrentPageNumber - 1;

        //public int NextPageNumber => this.CurrentPageNumber + 1;

        //public int ItemsPerPage { get; set; }

        //public int PagesCount => (int)Math.Ceiling((double)this.ClientsTotalCount / this.ItemsPerPage);

        //public int ClientsTotalCount { get; set; }

        public IEnumerable<SingleClientViewModel> Clients { get; set; }

    }
}
