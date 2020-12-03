namespace BankruptcyLaw.Web.ViewModels.Notes
{
    using System;

    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;

    public class NoteViewModel : IMapFrom<Note>
    {
        public string CaseId { get; set; }

        public string OriginalPoster { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
