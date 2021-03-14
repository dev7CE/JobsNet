using AutoMapper;
using data = Solution.DO.Objects;

namespace Solution.API.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile ()
        {
            CreateMap<data.Provincias, DataModels.Provincias>().ReverseMap();
        }
    }
}
