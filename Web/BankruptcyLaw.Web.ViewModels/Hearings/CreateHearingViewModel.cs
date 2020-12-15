namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BankruptcyLaw.Data.Models.Enums;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;

    public class CreateHearingViewModel : IMapTo<Hearing>
    {
        [Required]
        [MaxLength(200)]
        [MinLength(10)]
        public string HearingAddress { get; set; }

        public DateTime HearingDateAndTime { get; set; }

        public string CaseNumber { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(10)]
        public string Name { get; set; }

        public int LocalId { get; set; }

        public HearingOutcome? Outcome { get; set; }

        public string AttorneyId { get; set; }

        public string CaseId { get; set; }
    }
}
