namespace BankruptcyLaw.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using Microsoft.EntityFrameworkCore;

    public class AddressesService : IAddressesService
    {
        private IDeletableEntityRepository<Address> addressRepository;
        private IDeletableEntityRepository<ApplicationUser> usersRepository;

        public AddressesService(IDeletableEntityRepository<Address> addressRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.addressRepository = addressRepository;
            this.usersRepository = usersRepository;
        }

        public AddressViewModel GetAddressInfo(string username)
        {
            var addressId = this.usersRepository.All().FirstOrDefault(x => x.UserName == username).AddressId;

            var address = this.addressRepository.All().FirstOrDefault(x => x.Id == addressId);
            var result = new AddressViewModel();

            if (address == null)
            {
                result.City = string.Empty;
                result.State = string.Empty;
                result.ZipCode = string.Empty;
                result.StreetAddress = string.Empty;

                return result;
            }

            result = new AddressViewModel()
            {
                City = address.City,
                State = address.State,
                StreetAddress = address.StreetAddress,
                ZipCode = address.ZipCode.ToString(),
            };

            return result;
        }

        public async Task UpdateAddressInfo(string username, AddressViewModel input)
        {
            var user = this.usersRepository.All()
                .Where(x => x.UserName == username)
                .Include(x => x.Address)
                .FirstOrDefault();

            if (user.AddressId == null)
            {
                user.Address = new Address();
            }

            user.Address.State = input.State;
            user.Address.StreetAddress = input.StreetAddress;
            user.Address.City = input.City;
            user.Address.ZipCode = int.Parse(input.ZipCode);

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
