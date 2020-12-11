namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateHearingViewModel : IMapTo<Hearing>
    {
        public string CourtRoom { get; set; }

        public DateTime HearingDateAndTime { get; set; }

        public string Name { get; set; }

        public int LocalId { get; set; }

        public HearingOutcome Outcome { get; set; }

        public string AttorneyId { get; set; }

        public string CaseId { get; set; }
    }
}
