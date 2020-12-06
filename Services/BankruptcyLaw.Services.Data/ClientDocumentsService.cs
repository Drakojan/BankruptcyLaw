namespace BankruptcyLaw.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Documents;

    public class ClientDocumentsService : IClientDocumentsService
    {
        private readonly IDeletableEntityRepository<ClientDocument> clientDocumentsRepository;

        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public ClientDocumentsService(IDeletableEntityRepository<ClientDocument> clientDocumentsRepository)
        {
            this.clientDocumentsRepository = clientDocumentsRepository;
        }

        public async Task CreateAsync(UploadClientDocumentViewModel input, string userId, string imagePath)
        {
            Directory.CreateDirectory($"{imagePath}/clientDocuments/");

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new ClientDocument
                {
                    Size = (image.Length / 1000).ToString(), // size is in kb
                    AddedByUserId = userId,
                    CaseId = input.CaseId,
                    Name = input.Name,
                    Extension = extension,
                };

                await this.clientDocumentsRepository.AddAsync(dbImage);

                await this.clientDocumentsRepository.SaveChangesAsync();

                var physicalPath = $"{imagePath}/clientDocuments/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }
        }

        public AllClientDocumentsForCaseViewModel GetAllClientDocumentsForCase(string caseId)
        {
            var result = new AllClientDocumentsForCaseViewModel()
            {
                CaseId = caseId,
                Documents = this.clientDocumentsRepository.AllAsNoTracking()
                .Where(x => x.CaseId == caseId)
                .To<ClientDocumentViewModel>()
                .AsEnumerable(),
            };

            return result;
        }
    }
}
