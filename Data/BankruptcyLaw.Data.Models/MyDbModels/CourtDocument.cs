namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class CourtDocument : BaseDeletableModel<string>
    {
        public CourtDocument()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime DateIssued { get; set; }

        public string Extension { get; set; }

        public string CaseId { get; set; }

        public virtual Case Case { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
