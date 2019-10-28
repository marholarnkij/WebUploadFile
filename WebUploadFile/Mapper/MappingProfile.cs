using AutoMapper;
using Common.Lib.Entities.InputModels;
using Common.Lib.Entities.Models;
using Common.Lib.Entities.ReturnModels;
namespace WebUploadFile.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<JournalDetails, JournalReturnModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Amount.ToString("n2") + " " + src.CurrencyCode))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<JournalInput, JournalDetails>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionIdentifier))


                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                 ((src.Status.ToLower() == "approved") ? "A" :
                 (src.Status.ToLower() == "failed") ? "R" :
                 (src.Status.ToLower() == "finish") ? "D" : "")));

                //.ForMember(dest => dest.CreatedDate, opt =>
                //opt.MapFrom(src => System.DateTime.Now))
                //.ForMember(dest => dest.ModifiedDate, opt =>
                //opt.MapFrom(src => System.DateTime.Now));

        }
    }
}
