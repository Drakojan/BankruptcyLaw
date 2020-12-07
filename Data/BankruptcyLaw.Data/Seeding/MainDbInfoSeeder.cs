namespace BankruptcyLaw.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BankruptcyLaw.Data.Models.MyDbModels;

    internal class MainDbInfoSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Judges.Any())
            {
                await dbContext.Judges.AddAsync(new Judge
                {
                    FirstName = "Jacqueline",
                    LastName = "Cox",
                    CourtRoom = "Courtroom 680 at Everett McKinley Dirksen United States Courthouse",
                    Email = "jCoxChambers@court.com",
                    Phone = "312-435-5679",
                });

                await dbContext.Judges.AddAsync(new Judge
                {
                    FirstName = "Lashonda",
                    LastName = "Hunt",
                    CourtRoom = "Courtroom 719 at Everett McKinley Dirksen United States Courthouse",
                    Email = "LHuntChambers@court.com",
                    Phone = "312-435-6867",
                });
            }

            if (!dbContext.Trustees.Any())
            {
                await dbContext.Trustees.AddAsync(new Trustee
                {
                    FirstName = "Tom",
                    LastName = "Vaughn",
                    Email = "TVaughn@trusteeTV.com",
                    Phone = "312-435-5679",
                    AddressId = 3,
                });

                await dbContext.Trustees.AddAsync(new Trustee
                {
                    FirstName = "Marilyn",
                    LastName = "Marshall",
                    Email = "MMarshall@trusteeMM.com",
                    Phone = "312-435-4323",
                    AddressId = 4,
                });

            }

            if (!dbContext.Hearings.Any())
            {
                await dbContext.Hearings.AddAsync(new Hearing
                {
                    AttorneyId = "6f0d6e22-a86b-4998-bb7f-fde23e667076",
                    AddressId = 6,
                    HearingDateAndTime = DateTime.UtcNow,
                    LocalId = 1,
                    Name = "Confirmation Hearing",
                    Outcome = Models.HearingOutcome.Continued,
                });

                await dbContext.Hearings.AddAsync(new Hearing
                {
                    AttorneyId = "6f0d6e22-a86b-4998-bb7f-fde23e667076",
                    AddressId = 6,
                    HearingDateAndTime = DateTime.UtcNow,
                    LocalId = 2,
                    Name = "Motion for relief by Bank of America",
                    Outcome = Models.HearingOutcome.Granted,
                });
                await dbContext.Hearings.AddAsync(new Hearing
                {
                    AttorneyId = "6f0d6e22-a86b-4998-bb7f-fde23e667076",
                    AddressId = 6,
                    HearingDateAndTime = DateTime.UtcNow,
                    LocalId = 3,
                    Name = "Contnued Confirmation Hearing",
                    Outcome = Models.HearingOutcome.Granted,
                });
                await dbContext.Hearings.AddAsync(new Hearing
                {
                    AttorneyId = "6f0d6e22-a86b-4998-bb7f-fde23e667076",
                    AddressId = 6,
                    HearingDateAndTime = DateTime.UtcNow,
                    LocalId = 4,
                    Name = "Motion to Incur",
                    Outcome = Models.HearingOutcome.Denied,
                });
            }
        }
    }
}
