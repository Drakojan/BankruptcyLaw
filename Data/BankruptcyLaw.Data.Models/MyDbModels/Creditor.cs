namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Creditor : BaseDeletableModel<int>
    {
        public Creditor()
        {
            this.CreditorCases = new HashSet<CreditorCase>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<CreditorCase> CreditorCases { get; set; }
    }
}
