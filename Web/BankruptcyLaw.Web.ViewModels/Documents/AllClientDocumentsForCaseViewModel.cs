namespace BankruptcyLaw.Web.ViewModels.Documents
{
    using System.Collections.Generic;

    public class AllClientDocumentsForCaseViewModel
    {
        public string CaseId { get; set; }

        public IEnumerable<ClientDocumentViewModel> Documents { get; set; }
    }
}
