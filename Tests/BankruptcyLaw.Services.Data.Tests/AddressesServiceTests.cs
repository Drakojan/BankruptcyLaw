namespace BankruptcyLaw.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Data.Repositories;
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class AddressesServiceTests
    {
        [Fact]
        public void GetAddressInfoShouldWork()
        {
            AddressesService addressService = SetUpAddressService();

            var result = addressService.GetAddressInfoByUsername("Sofianec");

            Assert.Equal("Sofia", result.City);
            Assert.True(result.GetType() == typeof(AddressViewModel));
        }

        [Fact]
        public async Task UpdateAddressInfoWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdateAddressDb")
                // Give a Unique name to the DB
                .Options;

            using var dbContext = new ApplicationDbContext(options);
            // Initialize Testing Data

            dbContext.Addresses.AddRange(
                new Address()
                {
                    City = "Burgas",
                    ZipCode = 9000,
                    Id = 1,
                },
                new Address()
                {
                    City = "Sofia",
                    ZipCode = 5000,
                    Id = 2,
                });

            dbContext.Users.AddRange(
                new ApplicationUser()
                {
                    Id = "1",
                    UserName = "Niki",
                    AddressId = 1,
                },
                new ApplicationUser()
                {
                    Id = "2",
                    UserName = "Sofianec",
                    AddressId = 2,
                });

            dbContext.SaveChanges();

            using var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            using var addressesRepository = new EfDeletableEntityRepository<Address>(dbContext);

            var addressService = new AddressesService(addressesRepository, usersRepository);

            var newAddressInfo = new AddressViewModel()
            {
                City = "MovedToBurgas",
                ZipCode = "8000",
            };

            await addressService.UpdateAddressInfo("Sofianec", newAddressInfo);

            var currentAddressOfSofianec = addressService.GetAddressInfoByUsername("Sofianec");
            Assert.Equal("MovedToBurgas", currentAddressOfSofianec.City);
        }

        private static AddressesService SetUpAddressService()
        {
            var addressesList = new List<Address>
            {
                new Address()
                {
                    City = "Burgas",
                    ZipCode = 9000,
                    Id = 1,
                },

                new Address()
                {
                    City = "Sofia",
                    ZipCode = 5000,
                    Id = 2,
                },
            };

            var usersList = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    Id = "1",
                    UserName = "Niki",
                    AddressId = 1,
                    Address = new Address(),
                },
                new ApplicationUser()
                {
                    Id = "2",
                    UserName = "Sofianec",
                    AddressId = 2,
                    Address = new Address(),
                },
            };

            var addressesMockRepo = new Mock<IDeletableEntityRepository<Address>>();
            addressesMockRepo.Setup(x => x.All()).Returns(addressesList.AsQueryable());

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            usersMockRepo.Setup(x => x.All()).Returns(usersList.AsQueryable());

            var addressService = new AddressesService(addressesMockRepo.Object, usersMockRepo.Object);
            return addressService;
        }
    }
}
