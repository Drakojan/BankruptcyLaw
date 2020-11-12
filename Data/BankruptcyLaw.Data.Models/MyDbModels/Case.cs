namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System;
    using System.Collections.Generic;

    using BankruptcyLaw.Data.Common.Models;

    public class Case : BaseDeletableModel<string>, IDeletableEntity
    {
        public Case()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CaseStatus = CaseStatus.Active;
            this.CourtDocuments = new HashSet<CourtDocument>();
            this.ClientDocuments = new HashSet<ClientDocument>();
            this.CreditorCases = new HashSet<CreditorCase>();
            this.Hearings = new HashSet<Hearing>();
            this.Notes = new HashSet<Note>();
        }

        public string ClientId { get; set; }

        public virtual Client Client { get; set; }

        public string AttorneyId { get; set; }

        public virtual Attorney Attorney { get; set; }

        public int JudgeId { get; set; }

        public virtual Judge Judge { get; set; }

        public int TrusteeId { get; set; }

        public virtual Trustee Trustee { get; set; }

        public string CaseNumber { get; set; }

        public DateTime DateFiled { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public virtual ICollection<CourtDocument> CourtDocuments { get; set; }

        public virtual ICollection<ClientDocument> ClientDocuments { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<CreditorCase> CreditorCases { get; set; }

        public virtual ICollection<Hearing> Hearings { get; set; }
    }
}
