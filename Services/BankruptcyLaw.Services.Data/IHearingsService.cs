namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Hearings;

    public interface IHearingsService
    {
        public AllCaseHearingsViewModel GetHearingsForCaseId(string caseId);

        public AllAttorneyCaseHearingsViewModel GetHearingsForAttorneyId(string attorneyId);

        public Task CreateAsync(CreateHearingViewModel input);
    }
}
