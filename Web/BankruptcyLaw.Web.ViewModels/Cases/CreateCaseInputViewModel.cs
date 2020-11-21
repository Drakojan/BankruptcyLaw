namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCaseInputViewModel
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string AttorneyId { get; set; }

        public string JudgeId { get; set; }

        public string TrusteeId { get; set; }

        [Required]
        [Display(Name = "Please select the presiding Judge")]
        public IEnumerable<KeyValuePair<int, string>> Judges { get; set; }

        [Required]
        [Display(Name = "Please select the presiding Trustee")]
        public IEnumerable<KeyValuePair<int, string>> Trustees { get; set; }

        [Required]
        public string CaseNumber { get; set; }

        [PastDateAttribute(ErrorMessage = "The date is required and has to be in the past")]
        [Display(Name ="When was the case filed with the court?")]
        public DateTime DateFiled { get; set; }
    }
}
