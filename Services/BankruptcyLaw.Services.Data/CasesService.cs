namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;

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
    }
}
