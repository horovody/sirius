using Sirius.Data.Entities;

namespace Sirius.Logic.Dtos.Automapper
{
    public class SettingDtoProfile : AutoMapper.Profile
    {
        public SettingDtoProfile()
        {
            CreateMap<SettingEntity, SettingDto>()
                .ReverseMap();
        }
    }
}
