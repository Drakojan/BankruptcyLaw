namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Models.MyDbModels;

    public interface ITrusteesService
    {
        public IEnumerable<KeyValuePair<int, string>> GetTrusteesNamesAndIds();

        public Task<IEnumerable<Trustee>> GetAllTrusteesAsync();

        public Task<Trustee> GetTrusteeByIdAsync(int? id);

        public Task AddTrusteeAsync(Trustee trustee);

        public Task EditTrustee(Trustee trustee);

        public Task HardDeleteTrustee(Trustee trustee);
    }
}
