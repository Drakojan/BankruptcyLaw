namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BankruptcyLaw.Data.Common.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;

    public class Judge : BaseDeletableModel<int>
    {
        public Judge()
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

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public ICollection<Case> Cases { get; set; }

        public string CourtRoom { get; set; }
    }
}
