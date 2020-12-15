namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Hearings;

    public class HearingsService : IHearingsService
    {
        private IDeletableEntityRepository<Hearing> hearingsRepository;
        private IDeletableEntityRepository<Case> casesRepository;

        public HearingsService(IDeletableEntityRepository<Hearing> hearingsRepository, IDeletableEntityRepository<Case> casesRepository)
        {
            this.hearingsRepository = hearingsRepository;
            this.casesRepository = casesRepository;
        }

        public AllCaseHearingsViewModel GetHearingsForCaseId(string caseId)
        {
            var result = new AllCaseHearingsViewModel
            {
                CaseID = caseId,
            };

            result.Hearings = this.hearingsRepository.AllAsNoTracking()
                .Where(x => x.CaseId == caseId)
                .To<CaseHearingViewModel>()
                .OrderBy(x => x.LocalId)
                .AsEnumerable();

            return result;
        }

        public AllAttorneyCaseHearingsViewModel GetHearingsForAttorneyId(string attorneyId)
        {
            var result = new AllAttorneyCaseHearingsViewModel
            {
                AttorneyId = attorneyId,
            };

            var allHearings = this.hearingsRepository.AllAsNoTracking()
                .Where(x => x.AttorneyId == attorneyId && (x.HearingDateAndTime.Date >= DateTime.UtcNow.Date))
                .Select(x => new AttorneyCaseHearingViewModel
                {
                    CaseNumber = x.Case.CaseNumber,
                    HearingDateAndTime = x.HearingDateAndTime,
                    Name = x.Name,
                    HearingAddress = x.HearingAddress,
                })
                .ToList();

            result.HearingsGroupedByDate = allHearings
                .GroupBy(x => x.HearingDateAndTime.Date)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());

            // TODO: Probably not a good idea to use simple Dictionary here as I want the dates to be oredered. Test and switch to SortedDictionary if there are any issues.
            return result;
        }

        public async Task CreateAsync(CreateHearingViewModel input)
        {
            var caseIdAndHearingsCountInfo = this.casesRepository.AllAsNoTracking()
                .Where(x => x.CaseNumber == input.CaseNumber)
                .Take(1)
                .Select(x => new
                {
                    caseId = x.Id,
                    hearingsCount = x.Hearings.Count,
                })
                .FirstOrDefault();

            if (caseIdAndHearingsCountInfo == null)
            {
                throw new ArgumentNullException("caseNumber", "Case with the provided case number was not found in the database");
            }

            input.CaseId = caseIdAndHearingsCountInfo.caseId;
            input.LocalId = caseIdAndHearingsCountInfo.hearingsCount + 1;

            var mapper = AutoMapperConfig.MapperInstance;
            var newHearing = mapper.Map<Hearing>(input);

            await this.hearingsRepository.AddAsync(newHearing);
            await this.hearingsRepository.SaveChangesAsync();
        }
    }
}
