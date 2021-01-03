namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;

    public class ClientsService : IClientsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Case> casesRepository;

        public ClientsService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Case> casesRepository)
        {
            this.usersRepository = usersRepository;
            this.casesRepository = casesRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            {
                // new users are given the role client by default and have no other roles
                return this.usersRepository.AllAsNoTracking().Where(x => x.Roles.Count == 1)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                            .To<T>()
                            .ToList();
            }
        }

        public ApplicationUser GetClientByCaseId(string caseId)
        {
            var clientId = this.casesRepository.AllAsNoTracking().Where(x => x.Id == caseId).FirstOrDefault().ClientId;

            // var client = this.usersRepository.AllAsNoTracking().Where(x => x.Id == clientId).FirstOrDefault();

            var client = this.GetClientById(clientId);

            return client;
        }

        public ApplicationUser GetClientById(string clientId)
        {
            var client = this.usersRepository.AllAsNoTracking().Where(x => x.Id == clientId).FirstOrDefault();

            return client;
        }

        public int GetClientsTotal()
        {
            return this.usersRepository.AllAsNoTracking().Where(x => x.Roles.Count == 1).Count();
        }
    }
}
