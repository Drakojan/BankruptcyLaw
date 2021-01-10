namespace BankruptcyLaw.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Common.Repositories;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Cases;
    using BankruptcyLaw.Web.ViewModels.Notes;
    using Moq;
    using Xunit;

    public class NotesServiceTests
    {
        [Fact]
        public async Task CreateNoteTests()
        {
            var newNote = new NoteViewModel()
            {
                Content = "new note",
                CaseId = "1",
                OriginalPoster = "poster",
            };

            var notesList = new List<Note>();

            var mockNotesRepo = new Mock<IDeletableEntityRepository<Note>>();

            mockNotesRepo.Setup(x => x.AddAsync(It.IsAny<Note>()))
                .Callback((Note newNote) =>
                {
                    newNote.Id = "newDbId";
                    notesList.Add(newNote);
                });

            var notesService = new NotesService(mockNotesRepo.Object);

            var result = await notesService.CreateNoteAsync(newNote);

            Assert.Equal("newDbId", result.Id);
            Assert.Single(notesList);
        }

        [Fact]
        public void GetNotesForCaseTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(NoteViewModel).GetTypeInfo().Assembly);

            var notesList = new List<Note>()
            {
                new Note()
                {
                    CaseId = "1",
                    OriginalPoster = "Niki",
                    CreatedOn = DateTime.UtcNow,
                },
                new Note()
                {
                    CaseId = "1",
                    OriginalPoster = "Ivan",
                    CreatedOn = DateTime.UtcNow.AddDays(1),
                },
                new Note()
                {
                    CaseId = "2",
                    OriginalPoster = "Blagoi",
                    CreatedOn = DateTime.UtcNow,
                },
            };

            var mockNotesRepo = new Mock<IDeletableEntityRepository<Note>>();

            mockNotesRepo.Setup(x => x.AllAsNoTracking()).Returns(notesList.AsQueryable);

            var notesService = new NotesService(mockNotesRepo.Object);

            var result = notesService.GetNotesForCase("1");

            Assert.Equal(2, result.Count());

            Assert.Equal("Ivan", result.First().OriginalPoster);
        }
    }
}
