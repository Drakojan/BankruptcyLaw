namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using BankruptcyLaw.Data.Common.Models;

    public class Attorney : BaseDeletableModel<string>
    {
        public Attorney()
        {
            this.Cases = new HashSet<Case>();
            this.Hearings = new HashSet<Hearing>();
        }

        public ICollection<Case> Cases { get; set; }

        public ICollection<Hearing> Hearings { get; set; }

        [Required]
        public string AplicationUserId { get; set; }

        public virtual ApplicationUser AplicationUser { get; set; }
    }
}
