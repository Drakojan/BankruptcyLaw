namespace BankruptcyLaw.Services.Data
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Web.ViewModels.Notes;

    public interface INotesService
    {
        public Task CreateNoteAsync(CreateNoteInputViewModel input);
    }
}