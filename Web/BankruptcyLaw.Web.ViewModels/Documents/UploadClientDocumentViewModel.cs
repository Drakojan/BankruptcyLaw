namespace BankruptcyLaw.Web.ViewModels.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class UploadClientDocumentViewModel
    {
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        public string CaseId { get; set; }

        [Required(ErrorMessage ="Please name the document you're uploading. Name must be at least 2 symbols long")]
        [Display(Name="Tittle of uploaded Document")]
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
