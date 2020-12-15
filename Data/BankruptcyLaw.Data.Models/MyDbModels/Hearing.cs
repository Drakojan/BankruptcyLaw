namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;
    using BankruptcyLaw.Data.Models.Enums;

    public class Hearing : BaseDeletableModel<int>
    {
        public DateTime HearingDateAndTime { get; set; }

        public virtual Case Case { get; set; }

        public string CaseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string HearingAddress { get; set; }

        public HearingOutcome? Outcome { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public int LocalId { get; set; }

        public int? ContinuedHearingId { get; set; }

        public virtual Hearing ContinuedHearing { get; set; }

        public string AttorneyId { get; set; }

        public virtual ApplicationUser Attorney { get; set; }
    }
}
