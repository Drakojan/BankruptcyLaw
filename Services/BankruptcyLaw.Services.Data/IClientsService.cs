namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;

    public interface IClientsService
    {
        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        public int GetClientsTotal();
    }
}