namespace BankruptcyLaw.Web.ViewModels.Documents
{
    using System;

    using AutoMapper;
    using BankruptcyLaw.Data.Models.MyDbModels;
    using BankruptcyLaw.Services.Mapping;

    public class ClientDocumentViewModel : IMapFrom<ClientDocument>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Size { get; set; }

        public string Extension { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UploaderName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ClientDocument, ClientDocumentViewModel>()
                .ForMember(x => x.UploaderName, opt =>
                  opt.MapFrom(x => x.AddedByUser.FullName));
        }
    }
}