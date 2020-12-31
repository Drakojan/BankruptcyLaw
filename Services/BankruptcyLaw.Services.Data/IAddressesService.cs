namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Addresses;

    public interface IAddressesService
    {
        public AddressViewModel GetAddressInfoByUsername(string username);

        public Task UpdateAddressInfo(string username, AddressViewModel input);
    }
}