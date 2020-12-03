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

        public async Task<string> CreateCaseAsync(string clientId, CreateCaseInputViewModel input)
        {
            var mapper = AutoMapperConfig.MapperInstance;
            var newCase = mapper.Map<Case>(input);

            newCase.ClientId = clientId;

            await this.casesRepository.AddAsync(newCase);

            await this.casesRepository.SaveChangesAsync();

            return newCase.Id;
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

        public async Task DeleteCaseByIdAsync(string caseId)
        {
            var caseToDelete = this.casesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == caseId);

            this.casesRepository.Delete(caseToDelete);

            await this.casesRepository.SaveChangesAsync();
        }

        public CaseDetailsViewModel GetCaseById(string caseId)
        {
            var result = this.casesRepository.AllAsNoTracking()
                .Where(x => x.Id == caseId)
                .Select(x => new CaseDetailsViewModel()
                {
                    AttorneyName = x.Attorney.FullName,
                    DateFiled = x.DateFiled,
                    CaseNumber = x.CaseNumber,
                    CaseStatus = x.CaseStatus.ToString(),
                    JudgeName = x.Judge.FullName,
                    TrusteeName = x.Trustee.FullName,
                    CaseId = caseId,
                    ClientId = x.ClientId,
                })
                .FirstOrDefault();

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
