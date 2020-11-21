using BankruptcyLaw.Web.ViewModels.Clients;
using System.Collections.Generic;

namespace BankruptcyLaw.Services.Data
{
    public interface IClientsService
    {
        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage);
    }
}