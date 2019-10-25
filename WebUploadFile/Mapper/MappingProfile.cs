using AutoMapper;
using Common.Lib.Entities.Models;
using Common.Lib.Entities.ReturnModels;
namespace WebUploadFile.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<JournalDetails, JournalReturnModel>();
     }
    }
}
