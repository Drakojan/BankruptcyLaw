// ReSharper disable VirtualMemberCallInConstructor
namespace BankruptcyLaw.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BankruptcyLaw.Data.Common.Models;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Hearings = new HashSet<Hearing>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // 1 to 1 link with attorney OR client entity saved here if I wanted to go back to using two separate entities.
        // public string AttorneyUserId { get; set; }

        // public virtual Attorney AttorneyUser { get; set; }

        // public string ClientUserId { get; set; }

        // public virtual Client ClientUser { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => this.FirstName + " " + this.LastName;

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        // I think I won't need the bools below. 
        // public bool IsClient { get; set; } = true;

        // public bool IsAttorney { get; set; } = false;

        public ICollection<Hearing> Hearings { get; set; }

        // [RegularExpression(@"^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$")]
        public string SSN { get; set; }
    }
}
