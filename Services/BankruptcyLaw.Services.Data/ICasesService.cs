namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Cases;

    public interface ICasesService
    {
        public Task CreateCaseAsync(string clientId, CreateCaseInputViewModel input);
    }
}
