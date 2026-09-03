using AutoMapper;
using EventosApi.DTO;
using EventosApi.Models;

namespace EventosApi.BL.Profiles
{
    public class ParticipanteProfile : Profile
    {
        public ParticipanteProfile()
        {
            CreateMap<Participante, ParticipanteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdParticipante))
                .ReverseMap();
        }
    }
}
