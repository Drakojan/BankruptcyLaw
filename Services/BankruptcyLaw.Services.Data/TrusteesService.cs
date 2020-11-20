namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;

    public class TrusteesService : ITrusteesService
    {
        private readonly IDeletableEntityRepository<Trustee> trusteeRepository;

        public TrusteesService(IDeletableEntityRepository<Trustee> trusteeRepository)
        {
            this.trusteeRepository = trusteeRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetTrusteesNamesAndIds()
        {
            var result = this.trusteeRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    Name = $"{x.FirstName} {x.LastName}",
                })
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));

            return result;
        }
    }
}
