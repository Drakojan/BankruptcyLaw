namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using Microsoft.EntityFrameworkCore;

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

        // Administration services
        public async Task<IEnumerable<Trustee>> GetAllTrusteesAsync()
        {
            return await this.trusteeRepository.AllAsNoTrackingWithDeleted().ToListAsync();
        }

        public async Task<Trustee> GetTrusteeByIdAsync(int? id)
        {
            return await this.trusteeRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddTrusteeAsync(Trustee trustee)
        {
            await this.trusteeRepository.AddAsync(trustee);

            await this.trusteeRepository.SaveChangesAsync();
        }

        public async Task EditTrustee(Trustee trustee)
        {
            this.trusteeRepository.Update(trustee);
            await this.trusteeRepository.SaveChangesAsync();
        }

        public async Task HardDeleteTrustee(Trustee trustee)
        {
            this.trusteeRepository.Delete(trustee);

            await this.trusteeRepository.SaveChangesAsync();
        }
    }
}
