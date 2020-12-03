using BankruptcyLaw.Data.Common.Repositories;
using BankruptcyLaw.Data.Models.MyDbModels;
using BankruptcyLaw.Web.ViewModels.Notes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankruptcyLaw.Services.Data
{
    public class NotesService : INotesService
    {
        private IDeletableEntityRepository<Note> notesRepository;

        public NotesService(IDeletableEntityRepository<Note> notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        public async Task CreateNoteAsync(CreateNoteInputViewModel input)
        {
            var newNote = new Note()
            {
                Content = input.Content,
                CaseId = input.CaseId,
                OriginalPoster = input.CreatorName,
            };

            await this.notesRepository.AddAsync(newNote);
            await this.notesRepository.SaveChangesAsync();
        }
    }
}
