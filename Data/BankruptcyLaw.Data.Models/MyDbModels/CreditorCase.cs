namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using BankruptcyLaw.Data.Common.Models;

    public class CreditorCase : BaseModel<string>
    {
        public CreditorCase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int CreditorId { get; set; }

        public virtual Creditor Creditor { get; set; }

        public int CaseId { get; set; }

        public virtual Case Case { get; set; }

        public double AmountOwed { get; set; } = 0;

        public double AmountPaid { get; set; } = 0;

        [NotMapped]
        public double RemainingAmount => this.AmountOwed - this.AmountPaid;
    }
}
