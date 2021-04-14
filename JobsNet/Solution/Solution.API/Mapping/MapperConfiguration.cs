using AutoMapper;
using data = Solution.DO.Objects;

namespace Solution.API.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile ()
        {
            CreateMap<data.Provincias, DataModels.Provincias>().ReverseMap();
            CreateMap<data.Cantones, DataModels.Cantones>().ReverseMap();
            CreateMap<data.Usuarios, DataModels.Usuarios>().ReverseMap();
            CreateMap<data.Empresas, DataModels.Empresas>().ReverseMap();
            CreateMap<data.Oferentes, DataModels.Oferentes>().ReverseMap();
            CreateMap<data.PuestosTrabajo, DataModels.PuestosTrabajo>().ReverseMap();
            CreateMap<data.ListaOferentes, DataModels.ListaOferentes>().ReverseMap();
            CreateMap<data.Documentos, DataModels.Documentos>().ReverseMap();
        }
    }
}
