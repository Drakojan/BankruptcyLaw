namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Clients;

    public class ClientsService : IClientsService
    {
        private IDeletableEntityRepository<ApplicationUser> usersRepository;

        public ClientsService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            {

                return this.usersRepository.AllAsNoTracking().Where(x => x.Roles.Count == 1)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                            .To<T>()
                            .ToList();
            }

        }
    }
}
