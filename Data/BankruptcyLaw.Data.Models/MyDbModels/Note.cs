namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Note : BaseDeletableModel<string>
    {
        public Note()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string OriginalPoster { get; set; }

        public string RedactionPoster { get; set; }

        public string CaseId { get; set; }

        public virtual Case Case { get; set; }
    }
}