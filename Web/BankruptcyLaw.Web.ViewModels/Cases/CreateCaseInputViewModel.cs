namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateCaseInputViewModel
    {
        public string ClientId { get; set; }

        public string AttorneyId { get; set; }

        public string JudgeId { get; set; }

        public string TrusteeId { get; set; }

        public string CaseNumber { get; set; }

        // public string CaseStatus { get; set; }

        public string DateFiled { get; set; }
    }
}
