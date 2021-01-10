namespace BankruptcyLaw.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data;
    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Data.Repositories;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Cases;
    using BankruptcyLaw.Web.ViewModels.Documents;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ClientDocumentsServiceTests
    {
        [Fact]
        public async Task CreateAsyncDocumentTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateCaseInputViewModel).GetTypeInfo().Assembly);

            var clientDocumentsList = new List<ClientDocument>();

            var clientDocumentsMockRepo = new Mock<IDeletableEntityRepository<ClientDocument>>();

            clientDocumentsMockRepo.Setup(x => x.AddAsync(It.IsAny<ClientDocument>())).Callback(
                (ClientDocument clientDocument) =>
                {
                    clientDocument.Id = Guid.NewGuid().ToString();
                    clientDocumentsList.Add(clientDocument);
                });

            var file = new Mock<IFormFile>();

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // get current number of pictures in the directory so we can compare at the end
            Directory.CreateDirectory($"{desktopPath}/clientDocuments/");
            int filesCountBeginning = Directory.GetFiles(
                                        $"{desktopPath}/clientDocuments",
                                        "*",
                                        SearchOption.TopDirectoryOnly).Length;

            var sourceImg = File.OpenRead(desktopPath + "/SFA_scheme.png");

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = "Test.png";

            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            UploadClientDocumentViewModel input = new UploadClientDocumentViewModel()
            {
                CaseId = "1",
                Name = "test",
                Images = new List<IFormFile>() { file.Object },
            };

            var clientDocumentService = new ClientDocumentsService(clientDocumentsMockRepo.Object);

            var userId = "NewUserId";

            // test Happy path
            await clientDocumentService.CreateAsync(input, userId, desktopPath);

            Assert.Single(clientDocumentsList);
            file.Verify();

            int filesCountEnd = Directory.GetFiles(
                                  $"{desktopPath}/clientDocuments",
                                  "*",
                                  SearchOption.TopDirectoryOnly).Length;

            Assert.Equal(filesCountBeginning + 1, filesCountEnd);

            // test extensions filter
            var file2 = new Mock<IFormFile>();
            var fileName2 = "Test.pdf";

            file2.Setup(f => f.FileName).Returns(fileName2).Verifiable();

            UploadClientDocumentViewModel input2 = new UploadClientDocumentViewModel()
            {
                CaseId = "1",
                Name = "test",
                Images = new List<IFormFile>() { file2.Object },
            };

            await Assert.ThrowsAsync<Exception>(async () =>
            await clientDocumentService.CreateAsync(input2, userId, desktopPath));
        }

        [Fact]
        public void GetAllClientDocumentsForCaseTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ClientDocumentViewModel).GetTypeInfo().Assembly);

            var clientDocumentsList = new List<ClientDocument>()
            {
                new ClientDocument()
                {
                    CaseId = "1",
                    CreatedOn = DateTime.UtcNow,
                    AddedByUser = new ApplicationUser(),
                },
                new ClientDocument()
                {
                    CaseId = "1",
                    CreatedOn = DateTime.UtcNow,
                    AddedByUser = new ApplicationUser(),
                },
                new ClientDocument()
                {
                    CaseId = "2",
                    CreatedOn = DateTime.UtcNow,
                    AddedByUser = new ApplicationUser(),
                },
            };

            var clientDocumentsMockRepo = new Mock<IDeletableEntityRepository<ClientDocument>>();

            clientDocumentsMockRepo.Setup(x => x.AllAsNoTracking())
                .Returns(clientDocumentsList.AsQueryable);

            var clientDocumentService = new ClientDocumentsService(clientDocumentsMockRepo.Object);

            var result = clientDocumentService.GetAllClientDocumentsForCase("1");

            Assert.Equal(2, result.Documents.Count());
        }
    }
}
