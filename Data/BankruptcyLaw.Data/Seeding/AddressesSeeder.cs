namespace BankruptcyLaw.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Models.MyDbModels;

    internal class AddressesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Addresses.Any())
            {
                await dbContext.Addresses.AddAsync(new Address
                {
                    StreetAddress = "55 E. Monroe, Suite 3850",
                    City = "Chicago",
                    State = "Illinois (IL)",
                    ZipCode = 60603,
                });

                await dbContext.Addresses.AddAsync(new Address
                {
                    StreetAddress = "224 S Michigan Ave #800",
                    City = "Chicago",
                    State = "Illinois (IL)",
                    ZipCode = 60604,
                });
            }
        }
    }
}