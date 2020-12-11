namespace BankruptcyLaw.Services.Data
{
    using BankruptcyLaw.Web.ViewModels.Hearings;

    public interface IHearingsService
    {
        public AllCaseHearingsViewModel GetHearingsForCaseId(string caseId);

        public AllAttorneyCaseHearingsViewModel GetHearingsForAttorneyId(string attorneyId);
    }
}