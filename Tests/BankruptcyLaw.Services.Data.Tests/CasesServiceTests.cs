namespace BankruptcyLaw.Services.Data.Tests
{
    using System;
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
    using BankruptcyLaw.Web.ViewModels.Cases;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CasesServiceTests
    {
        [Fact]
        public async Task CreateCaseTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateCaseInputViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateCaseDb")
                .Options;

            using var dbContext = new ApplicationDbContext(options);

            dbContext.Cases.AddRange(
                new Case()
                {
                    CaseNumber = "1",
                    AttorneyId = "1",
                    ClientId = "1",
                    JudgeId = 1,
                    TrusteeId = 1,
                    DateFiled = DateTime.UtcNow,
                });

            dbContext.SaveChanges();

            using var casesRepository = new EfDeletableEntityRepository<Case>(dbContext);
            using var notesRepository = new EfDeletableEntityRepository<Note>(dbContext);

            var notesService = new NotesService(notesRepository);
            var casesService = new CasesService(casesRepository, notesService);

            var workingInput = new CreateCaseInputViewModel()
            {
                CaseNumber = "2",
                AttorneyId = "2",
                JudgeId = "2",
                TrusteeId = "2",
                DateFiled = DateTime.UtcNow,
            };

            var duplicateCaseNumberInput = new CreateCaseInputViewModel()
            {
                CaseNumber = "1",
                AttorneyId = "2",
                JudgeId = "2",
                TrusteeId = "2",
                DateFiled = DateTime.UtcNow,
            };

            var caseId = await casesService.CreateCaseAsync("newClient", workingInput);

            Assert.True(caseId is string);

            await Assert.ThrowsAsync<ArgumentException>(
                async () =>
                {
                    await casesService.CreateCaseAsync("newClient", duplicateCaseNumberInput);
                });

            Assert.Equal(2, casesRepository.AllAsNoTracking().Count());

        }

        [Fact]
        public void GetAllCasesForClientTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase(databaseName: "TestGetAllCasesForClientDb")
.Options;

            using var dbContext = new ApplicationDbContext(options);

            dbContext.Cases.AddRange(
                new Case()
                {
                    CaseNumber = "1",
                    Attorney = new ApplicationUser()
                    {
                        FirstName = "niki",
                        LastName = "mitrev",
                    },
                    ClientId = "1",
                    Judge = new Judge(),
                    Trustee = new Trustee(),
                    DateFiled = DateTime.UtcNow,
                },
                new Case()
                {
                    CaseNumber = "3",
                    Attorney = new ApplicationUser()
                    {
                        FirstName = "niki",
                        LastName = "mitrev",
                    },
                    ClientId = "1",
                    Judge = new Judge(),
                    Trustee = new Trustee(),
                    DateFiled = DateTime.UtcNow.AddDays(1),
                });

            dbContext.SaveChanges();

            using var casesRepository = new EfDeletableEntityRepository<Case>(dbContext);
            using var notesRepository = new EfDeletableEntityRepository<Note>(dbContext);

            var notesService = new NotesService(notesRepository);
            var casesService = new CasesService(casesRepository, notesService);

            AllClientCasesViewModel result = casesService.GetAllCasesForClient("1", "Gosho");

            var countOfCases = result.Cases.Count();

            var clientName = result.ClientName;

            var caseOnTop = result.Cases.First();

            Assert.Equal(2, countOfCases);
            Assert.Equal("Gosho", clientName);
            Assert.Equal("3", caseOnTop.CaseNumber);
        }

        [Fact]
        public async Task DeleteCaseByIdTests()
        {
            var listOfCases = new List<Case>()
            {
                new Case()
                {
                    Id = "1",
                },
                new Case()
                {
                    Id = "2",
                },
                new Case()
                {
                    Id = "3",
                },
            };
            var mockCasesRepo = new Mock<IDeletableEntityRepository<Case>>();
            var mockNotesRepo = new Mock<IDeletableEntityRepository<Note>>();

            mockCasesRepo.Setup(r => r.AllAsNoTracking()).Returns(listOfCases.AsQueryable);
            mockCasesRepo.Setup(x => x.Delete(It.IsAny<Case>())).Callback(
                (Case caseToDelete) => listOfCases
                .Where(x => x.Id == caseToDelete.Id)
                .FirstOrDefault().IsDeleted = true);

            var notesService = new NotesService(mockNotesRepo.Object);
            var casesService = new CasesService(mockCasesRepo.Object, notesService);

            await casesService.DeleteCaseByIdAsync("1");

            Assert.Equal(3, listOfCases.Count());
            Assert.True(listOfCases.Where(x => x.Id == "1").FirstOrDefault().IsDeleted);
        }

        [Fact]
        public void GetCaseByIdTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateCaseInputViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetCaseByIdDb")
                .Options;

            using var dbContext = new ApplicationDbContext(options);

            dbContext.Cases.AddRange(
                new Case()
                {
                    Id = "newId",
                    CaseNumber = "1",
                    Attorney = new ApplicationUser()
                    {
                        FirstName = "niki",
                        LastName = "mitrev",
                    },
                    ClientId = "1",
                    Judge = new Judge(),
                    Trustee = new Trustee(),
                    DateFiled = DateTime.UtcNow,
                },
                new Case()
                {
                    CaseNumber = "3",
                    Attorney = new ApplicationUser()
                    {
                        FirstName = "niki",
                        LastName = "mitrev",
                    },
                    ClientId = "1",
                    Judge = new Judge(),
                    Trustee = new Trustee(),
                    DateFiled = DateTime.UtcNow.AddDays(1),
                });

            dbContext.SaveChanges();

            using var casesRepository = new EfDeletableEntityRepository<Case>(dbContext);
            using var notesRepository = new EfDeletableEntityRepository<Note>(dbContext);

            var notesService = new NotesService(notesRepository);
            var casesService = new CasesService(casesRepository, notesService);

            var result = casesService.GetCaseById("newId");

            Assert.True(result.GetType() == typeof(CaseDetailsViewModel));
            Assert.Equal("1", result.CaseNumber);
        }
    }
}
