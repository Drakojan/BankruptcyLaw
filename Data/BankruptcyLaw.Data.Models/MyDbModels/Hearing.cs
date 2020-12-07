namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Hearing : BaseDeletableModel<int>
    {
        public DateTime HearingDateAndTime { get; set; }

        public string CaseId { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public HearingOutcome? Outcome { get; set; }

        public string Name { get; set; }

        public int LocalId { get; set; }

        public int? ContinuedHearingId { get; set; }

        public virtual Hearing ContinuedHearing { get; set; }

        public string AttorneyId { get; set; }

        public virtual ApplicationUser Attorney { get; set; }
    }
}
