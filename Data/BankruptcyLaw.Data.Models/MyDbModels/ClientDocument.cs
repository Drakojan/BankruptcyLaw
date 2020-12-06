namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class ClientDocument : BaseDeletableModel<string>
    {
        public ClientDocument()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Extension { get; set; }

        public string Size { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public string CaseId { get; set; }

        public virtual Case Case { get; set; }
    }
}
