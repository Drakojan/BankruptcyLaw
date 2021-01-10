namespace BankruptcyLaw.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using Moq;
    using Xunit;

    public class JudgesServiceTests
    {
        [Fact]
        public void GetJudgesNamesAndIdsTests()
        {
            var judgesList = new List<Judge>()
            {
                new Judge()
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Bogorov",
                },
                new Judge()
                {
                    Id = 2,
                    FirstName = "Ivan2",
                    LastName = "Bogorov2",
                },
            };
            var mockJudgesRepo = new Mock<IDeletableEntityRepository<Judge>>();
            mockJudgesRepo.Setup(x => x.AllAsNoTracking()).Returns(judgesList.AsQueryable);

            var judgesService = new JudgesService(mockJudgesRepo.Object);
            var result = judgesService.GetJudgesNamesAndIds();

            Assert.Equal(2, result.Count());

            // not sure how to check if the output type is correct. It always seem to come out as System.RuntimeType
            // Assert.IsType<KeyValuePair<int, string>>(result.First().GetType());
        }
    }
}
