namespace BankruptcyLaw.Web.ViewModels.Notes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateNoteInputViewModel
    {
        public string CaseId { get; set; }

        public string CreatorName { get; set; }

        public string Content { get; set; }
    }
}
