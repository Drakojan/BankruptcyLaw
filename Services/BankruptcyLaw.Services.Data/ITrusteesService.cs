using System.Collections.Generic;

namespace BankruptcyLaw.Services.Data
{
    public interface ITrusteesService
    {
        public IEnumerable<KeyValuePair<int, string>> GetTrusteesNamesAndIds();
    }
}