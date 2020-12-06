namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Documents;

    public interface IClientDocumentsService
    {
        public Task CreateAsync(UploadClientDocumentViewModel input, string userId, string imagePath);

        public AllClientDocumentsForCaseViewModel GetAllClientDocumentsForCase(string caseId);
    }
}
