namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllClientCasesViewModel
    {
        public IEnumerable<SingleCaseViewModel> Cases { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }
    }
}
