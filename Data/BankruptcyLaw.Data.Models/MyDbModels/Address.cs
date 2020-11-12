namespace BankruptcyLaw.Data.Models.MyDbModels
{
    using System.ComponentModel.DataAnnotations;

    using BankruptcyLaw.Data.Common.Models;

    public class Address : BaseDeletableModel<int>, IAuditInfo
    {
        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(30)]
        public string State { get; set; }

        [Required]
        [MaxLength(30)]
        public int ZipCode { get; set; }
    }
}
