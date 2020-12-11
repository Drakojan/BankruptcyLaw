namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;

    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Hearings;

    public class HearingsService : IHearingsService
    {
        private IDeletableEntityRepository<Hearing> hearingsRepository;

        public HearingsService(IDeletableEntityRepository<Hearing> hearingsRepository)
        {
            this.hearingsRepository = hearingsRepository;
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
                })
                .ToList();

            result.HearingsGroupedByDate = allHearings
                .GroupBy(x => x.HearingDateAndTime.Date)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());

            // TODO: Probably not a good idea to use simple Dictionary here as I want the dates to be oredered. Test and switch to SortedDictionary if there are any issues.
            return result;
        }
    }
}
