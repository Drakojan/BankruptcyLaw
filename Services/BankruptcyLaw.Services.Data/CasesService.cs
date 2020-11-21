namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Web.ViewModels.Cases;

    public class CasesService : ICasesService
    {
        private IDeletableEntityRepository<Case> casesRepository;

        public CasesService(IDeletableEntityRepository<Case> casesRepository)
        {
            this.casesRepository = casesRepository;
        }

        public void CreateCase()
        {

        }

        public IEnumerable<AllCasesViewModelPagination> GetAll(int page, int itemsPerPage)
        {
            var result = this.casesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            throw new NotImplementedException();
        }
    }
}
