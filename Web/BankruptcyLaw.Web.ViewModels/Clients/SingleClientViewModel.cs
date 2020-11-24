namespace BankruptcyLaw.Web.ViewModels.Clients
{
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Mapping;
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SingleClientViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SSN { get; set; }
    }
}
