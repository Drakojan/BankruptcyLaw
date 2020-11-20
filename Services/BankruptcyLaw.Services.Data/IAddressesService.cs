namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Addresses;

    public interface IAddressesService
    {
        public AddressViewModel GetAddressInfo(string username);

        public Task UpdateAddressInfo(string username, AddressViewModel input);
    }
}