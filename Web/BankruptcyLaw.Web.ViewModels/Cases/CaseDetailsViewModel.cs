namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CaseDetailsViewModel
    {
        public string CaseId { get; set; }

        [Display(Name = "Attorney")]
        public string AttorneyName { get; set; }

        [Display(Name = "Judge")]
        public string JudgeName { get; set; }

        [Display(Name = "Trustee")]
        public string TrusteeName { get; set; }

        [Display(Name = "Case Status")]
        public string CaseStatus { get; set; }

        [Display(Name = "Case Number")]
        public string CaseNumber { get; set; }

        [Display(Name = "Date Filed")]
        public DateTime DateFiled { get; set; }
    }
}
