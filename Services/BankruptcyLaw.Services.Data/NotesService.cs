namespace BankruptcyLaw.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Notes;

    public class NotesService : INotesService
    {
        private IDeletableEntityRepository<Note> notesRepository;

        public NotesService(IDeletableEntityRepository<Note> notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        public async Task<Note> CreateNoteAsync(NoteViewModel input)
        {
            var newNote = new Note()
            {
                Content = input.Content,
                CaseId = input.CaseId,
                OriginalPoster = input.OriginalPoster,
            };

            await this.notesRepository.AddAsync(newNote);
            await this.notesRepository.SaveChangesAsync();

            return newNote;
        }

        public IEnumerable<NoteViewModel> GetNotesForCase(string caseId)
        {
            return this.notesRepository.AllAsNoTracking()
                .Where(x => x.CaseId == caseId)
                .To<NoteViewModel>()
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }
    }
}
