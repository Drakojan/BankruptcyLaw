namespace BankruptcyLaw.Web.ViewModels.Clients
{
    using BankruptcyLaw.Data.Models;
    using BankruptcyLaw.Services.Mapping;

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
