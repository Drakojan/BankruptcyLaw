namespace BankruptcyLaw.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Notes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : BaseController
    {
        private readonly INotesService notesService;

        public NotesController(INotesService notesService)
        {
            this.notesService = notesService;
        }

        [HttpPost]
        [Authorize(Roles = "Attorney")]
        public async Task<ActionResult<NoteCreationResponseModel>> Create(NoteViewModel input)
        {
            var username = this.User.Identity.Name;
            input.OriginalPoster = username;

            var newNote = await this.notesService.CreateNoteAsync(input);

            return new NoteCreationResponseModel
            {
                Author = username,
                Content = newNote.Content,
                CreatedOn = newNote.CreatedOn.ToString("MM/dd/yyyy hh:mm tt").Trim(),
            };

        }
    }
}
