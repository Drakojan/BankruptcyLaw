namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
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

        public CaseHearingsViewModel GetHearingsForCaseId(string caseId)
        {
            var result = new CaseHearingsViewModel
            {
                CaseID = caseId,
            };

            result.Hearings = this.hearingsRepository.AllAsNoTracking()
                .Where(x => x.CaseId == caseId)
                .To<HearingViewModel>()
                .AsEnumerable();

            return result;
        }
    }
}
