namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using BankruptcyLaw.Data.Common.Models;

    public class Client : BaseDeletableModel<string>
    {
        public Client()
        {
            this.Cases = new HashSet<Case>();
        }

        // [RegularExpression(@"^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$")]
        public string SSN { get; set; }

        public ICollection<Case> Cases { get; set; }

        [Required]
        public string AplicationUserId { get; set; }

        public virtual ApplicationUser AplicationUser { get; set; }
    }
}
