using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    public class CaseHearingsViewModel
    {
        public IEnumerable<HearingViewModel> Hearings { get; set; }

        public string CaseID { get; set; }
    }
}
