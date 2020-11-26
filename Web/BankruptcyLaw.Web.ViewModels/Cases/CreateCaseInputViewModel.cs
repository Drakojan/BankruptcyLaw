namespace BankruptcyLaw.Web.ViewModels.Cases
{
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCaseInputViewModel : IMapTo<Case>
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string AttorneyId { get; set; }

        [Required]
        [Display(Name = "Please select the presiding Judge")]
        public string JudgeId { get; set; }

        [Required]
        [Display(Name = "Please select the presiding Trustee")]
        public string TrusteeId { get; set; }

        public IEnumerable<KeyValuePair<int, string>> Judges { get; set; }

        public IEnumerable<KeyValuePair<int, string>> Trustees { get; set; }

        [Required]
        [Display(Name = "Case Number")]
        public string CaseNumber { get; set; }

        [PastDateAttribute(ErrorMessage = "The date is required and has to be in the past")]
        [Display(Name ="When was the case filed with the court?")]
        public DateTime DateFiled { get; set; }
    }
}
