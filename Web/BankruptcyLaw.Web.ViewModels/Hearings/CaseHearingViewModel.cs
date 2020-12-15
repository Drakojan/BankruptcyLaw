namespace BankruptcyLaw.Web.ViewModels.Hearings
{
    using System;

    using AutoMapper;
    using BankruptcyLaw.Data.Models.Enums;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;

    public class CaseHearingViewModel : IMapFrom<Hearing>, IHaveCustomMappings
    {
        public string HearingAddress { get; set; }

        public DateTime HearingDateAndTime { get; set; }

        public string Name { get; set; }

        public int LocalId { get; set; }

        public HearingOutcome Outcome { get; set; }

        public int? ContinuedToLocalId { get; set; }

        public string AttorneyId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Hearing, CaseHearingViewModel>()
                .ForMember(x => x.ContinuedToLocalId, opt =>
                  opt.MapFrom(y => y.ContinuedHearing.LocalId));
        }
    }
}
