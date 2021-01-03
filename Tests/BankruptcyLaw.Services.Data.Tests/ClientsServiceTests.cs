namespace BankruptcyLaw.Services.Data.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Data.Repositories;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using BankruptcyLaw.Web.ViewModels.Cases;
    using BankruptcyLaw.Web.ViewModels.Clients;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ClientsServiceTests
    {
        [Fact]
        public void GetAllTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateCaseInputViewModel).GetTypeInfo().Assembly);

            var mockUser1 = new Mock<ApplicationUser>();
            mockUser1.Setup(x => x.Roles.Count).Returns(1);
            mockUser1.Setup(x => x.CreatedOn).Returns(DateTime.UtcNow);
            mockUser1.Setup(x => x.Id).Returns("1");

            var mockUser2 = new Mock<ApplicationUser>();
            mockUser2.Setup(x => x.Roles.Count).Returns(2);
            mockUser2.Setup(x => x.CreatedOn).Returns(DateTime.UtcNow);
            mockUser2.Setup(x => x.Id).Returns("2");

            var mockUser3 = new Mock<ApplicationUser>();
            mockUser3.Setup(x => x.Roles.Count).Returns(1);
            mockUser3.Setup(x => x.CreatedOn).Returns(DateTime.UtcNow.AddDays(1));
            mockUser3.Setup(x => x.Id).Returns("3");

            var mockUser4 = new Mock<ApplicationUser>();
            mockUser4.Setup(x => x.Roles.Count).Returns(1);
            mockUser4.Setup(x => x.CreatedOn).Returns(DateTime.UtcNow.AddDays(2));
            mockUser4.Setup(x => x.Id).Returns("4");

            var clientsList = new List<ApplicationUser>();
            clientsList.Add(mockUser1.Object);
            clientsList.Add(mockUser2.Object);
            clientsList.Add(mockUser3.Object);
            clientsList.Add(mockUser4.Object);

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();

            usersMockRepo.Setup(x => x.AllAsNoTracking())
                .Returns(clientsList.AsQueryable);

            var clientsService = new ClientsService(usersMockRepo.Object, null);

            var result = clientsService.GetAll<SingleClientViewModel>(1, 2);

            Assert.Equal("4", result.First().Id);
            Assert.Equal(2, result.Count());

            var result2 = clientsService.GetAll<SingleClientViewModel>(2, 2);
            Assert.Single(result2);
        }

        [Fact]
        public void GetClientByCaseIdTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateCaseInputViewModel).GetTypeInfo().Assembly);

            // prep users
            var mockUser1 = new Mock<ApplicationUser>();
            mockUser1.Setup(x => x.Id).Returns("1");

            var mockUser2 = new Mock<ApplicationUser>();
            mockUser2.Setup(x => x.Id).Returns("2");

            var clientsList = new List<ApplicationUser>();
            clientsList.Add(mockUser1.Object);
            clientsList.Add(mockUser2.Object);

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();

            usersMockRepo.Setup(x => x.AllAsNoTracking())
                .Returns(clientsList.AsQueryable);

            // prep cases
            var mockCase1 = new Mock<Case>();
            mockCase1.Setup(x => x.Id).Returns("1");
            mockCase1.Setup(x => x.ClientId).Returns("1");

            var mockCase2 = new Mock<Case>();
            mockCase2.Setup(x => x.Id).Returns("2");
            mockCase2.Setup(x => x.ClientId).Returns("5");

            var casesList = new List<Case>();
            casesList.Add(mockCase1.Object);
            casesList.Add(mockCase2.Object);

            var casesMockRepo = new Mock<IDeletableEntityRepository<Case>>();

            casesMockRepo.Setup(x => x.AllAsNoTracking())
                .Returns(casesList.AsQueryable);

            var clientsService = new ClientsService(usersMockRepo.Object, casesMockRepo.Object);

            var result = clientsService.GetClientByCaseId("1");

            Assert.Equal("1", result.Id);
        }
    }
}
