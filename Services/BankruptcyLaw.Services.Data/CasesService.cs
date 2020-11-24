namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Web.ViewModels.Cases;

    using AutoMapper;
    using System.Threading.Tasks;
    using BankruptcyLaw.Services.Mapping;

    public class CasesService : ICasesService
    {
        private IDeletableEntityRepository<Case> casesRepository;

        public CasesService(IDeletableEntityRepository<Case> casesRepository)
        {
            this.casesRepository = casesRepository;
        }

        public async Task CreateCaseAsync(string clientId, CreateCaseInputViewModel input)
        {
            var mapper = AutoMapperConfig.MapperInstance;
            var newCase = mapper.Map<Case>(input);

            newCase.ClientId = clientId;

            await this.casesRepository.AddAsync(newCase);

            await this.casesRepository.SaveChangesAsync();
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
