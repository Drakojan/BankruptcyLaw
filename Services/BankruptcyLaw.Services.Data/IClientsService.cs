namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;

    using BankruptcyLaw.Data.Models;

    public interface IClientsService
    {
        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        public ApplicationUser GetClientByCaseId(string caseId);

        public ApplicationUser GetClientById(string clientId);

        public int GetClientsTotal();
    }
}
