namespace BankruptcyLaw.Web.Controllers
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Common;
    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Services.Messaging;
    using BankruptcyLaw.Web.ViewModels.Notes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : BaseController
    {
        private readonly INotesService notesService;
        private readonly IEmailSender sender;
        private readonly IClientsService clientService;

        public NotesController(INotesService notesService, IEmailSender sender, IClientsService clientService)
        {
            this.notesService = notesService;
            this.sender = sender;
            this.clientService = clientService;
        }

        [HttpPost]
        [Authorize(Roles = "Attorney")]
        public async Task<ActionResult<NoteCreationResponseModel>> Create(NoteViewModel input)
        {
            var username = this.User.Identity.Name;
            input.OriginalPoster = username;

            var newNote = await this.notesService.CreateNoteAsync(input);

            var clientEmail = this.clientService.GetClientByCaseId(input.CaseId).Email;

            await this.sender.SendEmailAsync("drakojan@mail.bg", "BkAdministration", clientEmail /*"nikolay.mitrev1@gmail.com"*/, "New Note added to your case", string.Format(GlobalConstants.EmailInfo, newNote.Content));

            // Use this email instead of client's email to test "nikolay.mitrev1@gmail.com"
            return new NoteCreationResponseModel
            {
                Author = username,
                Content = newNote.Content,
                CreatedOn = newNote.CreatedOn.ToString("MM/dd/yyyy hh:mm tt").Trim(),
            };
        }
    }
}
