namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
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
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => this.FirstName + " " + this.LastName;

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Case> Cases { get; set; }
    }
}