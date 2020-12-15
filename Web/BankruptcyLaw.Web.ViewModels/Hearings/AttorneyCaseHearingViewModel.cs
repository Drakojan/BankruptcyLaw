namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using System;

    public class AttorneyCaseHearingViewModel
    {
        public string CaseNumber { get; set; }

        public string Name { get; set; }

        public DateTime HearingDateAndTime { get; set; }

        public string HearingAddress { get; set; }
    }
}
