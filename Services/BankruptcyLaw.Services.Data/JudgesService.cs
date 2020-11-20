namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;

    public class JudgesService : IJudgesService
    {
        private readonly IDeletableEntityRepository<Judge> judgesRepository;

        public JudgesService(IDeletableEntityRepository<Judge> judgesRepository)
        {
            this.judgesRepository = judgesRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetJudgesNamesAndIds()
        {
            var result = this.judgesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    Name = $"{x.FirstName} {x.LastName}",
                })
                .ToList()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Name));

            return result;
        }
    }
}
