namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Cases;

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

        public AllClientCasesViewModel GetAllCasesForClient(string clientId, string clientName)
        {
            var cases = this.casesRepository.AllAsNoTracking()
                .Where(x => x.ClientId == clientId)
                .Select(x => new SingleCaseViewModel()
                {
                    CaseStatus = x.CaseStatus.ToString(),
                    CaseId = x.Id,
                    CaseNumber = x.CaseNumber,
                    AttorneyName = x.Attorney.FullName,
                    JudgeName = x.Judge.FullName,
                    TrusteeName = x.Trustee.FullName,
                    DateFiled = x.DateFiled,
                })
                .ToList()
                .OrderByDescending(x => x.DateFiled);

            var result = new AllClientCasesViewModel()
            {
                ClientId = clientId,
                Cases = cases,
                ClientName = clientName,
            };

            return result;
        }

        // pagination prospect
        public IEnumerable<AllClientCasesViewModel> GetAll(int page, int itemsPerPage)
        {
            var result = this.casesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            throw new NotImplementedException();
        }
    }
}
