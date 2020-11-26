namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Cases;

    public interface ICasesService
    {
        public Task<string> CreateCaseAsync(string clientId, CreateCaseInputViewModel input);

        public AllClientCasesViewModel GetAllCasesForClient(string clientId, string clientName);

        public Task DeleteCaseByIdAsync(string caseId);
    }
}
