namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using System.Collections.Generic;

    public class AllCaseHearingsViewModel
    {
        public IEnumerable<CaseHearingViewModel> Hearings { get; set; }

        public string CaseID { get; set; }
    }
}
