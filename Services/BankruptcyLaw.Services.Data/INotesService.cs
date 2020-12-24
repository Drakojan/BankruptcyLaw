namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Web.ViewModels.Notes;

    public interface INotesService
    {
        public Task<Note> CreateNoteAsync(NoteViewModel input);

        public IEnumerable<NoteViewModel> GetNotesForCase(string caseId);
    }
}