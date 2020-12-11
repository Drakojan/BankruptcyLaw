namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using System;
    using System.Collections.Generic;

    public class AllAttorneyCaseHearingsViewModel
    {
        public Dictionary<DateTime, IEnumerable<AttorneyCaseHearingViewModel>> HearingsGroupedByDate { get; set; }

        public string AttorneyId { get; set; }
    }
}
