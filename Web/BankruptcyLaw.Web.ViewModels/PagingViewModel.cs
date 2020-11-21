namespace BankruptcyLaw.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int CurrentPageNumber { get; set; }

        public bool HasPreviousPage => this.CurrentPageNumber > 1;

        public int PreviousPageNumber => this.CurrentPageNumber - 1;

        public bool HasNextPage => this.CurrentPageNumber < this.PagesCount;

        public int NextPageNumber => this.CurrentPageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ClientsTotalCount / this.ItemsPerPage);

        public int ClientsTotalCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}