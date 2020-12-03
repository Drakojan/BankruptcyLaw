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
        [Authorize(Roles="Attorney")]
        public async Task<IActionResult> Create(NoteViewModel input)
        {
            var username = this.User.Identity.Name;
            input.OriginalPoster = username;

            await this.notesService.CreateNoteAsync(input);
            return this.Ok();
        }
    }
}
