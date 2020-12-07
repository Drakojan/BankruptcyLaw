using BankruptcyLaw.Web.ViewModels.Hearings;

namespace BankruptcyLaw.Services.Data
{
    public interface IHearingsService
    {
        public CaseHearingsViewModel GetHearingsForCaseId(string caseId);
    }
}