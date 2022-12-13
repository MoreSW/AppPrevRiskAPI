using appPrevencionRiesgos.Data.Entities;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;
using AutoMapper;

namespace appPrevencionRiesgos.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<UserInformationEntity, UserInformationModel>()
                .ReverseMap();
            this.CreateMap<LocationEntity, LocationModel>()
                .ReverseMap();
        }
    }
}
