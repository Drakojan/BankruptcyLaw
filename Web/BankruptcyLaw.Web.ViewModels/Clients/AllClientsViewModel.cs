namespace BankruptcyLaw.Web.ViewModels.Clients
{
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllClientsViewModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SSN { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
