namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Hearing : BaseDeletableModel<int>
    {
        public DateTime HearingDateAndTime { get; set; }

        [Required]
        public Address Address { get; set; }

        public HearingOutcome? Outcome { get; set; }

        public int? ContinuedHearingId { get; set; }

        public virtual Hearing ContinuedHearing { get; set; }

        public string AttorneyId { get; set; }

        public virtual ApplicationUser Attorney { get; set; }
    }
}
