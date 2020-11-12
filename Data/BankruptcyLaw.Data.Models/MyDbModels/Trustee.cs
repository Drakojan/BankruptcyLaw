namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Trustee : BaseDeletableModel<int>
    {
        public Trustee()
        {
            this.Cases = new HashSet<Case>();
        }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LasttName { get; set; }

        public Address Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Case> Cases { get; set; }
    }
}