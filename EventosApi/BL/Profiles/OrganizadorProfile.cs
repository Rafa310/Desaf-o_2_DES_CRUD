using AutoMapper;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL.Profiles
{
    public class OrganizadorProfile : Profile
    {
        public OrganizadorProfile()
        {
            CreateMap<Organizador, OrganizadorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdOrganizador))
                .ReverseMap();
        }
    }
}
