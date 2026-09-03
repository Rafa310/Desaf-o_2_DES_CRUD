using AutoMapper;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL.Profiles
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdEvento))
                .ReverseMap();
        }
    }
}
