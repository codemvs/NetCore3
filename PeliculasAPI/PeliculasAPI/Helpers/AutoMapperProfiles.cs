using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            // Genero < - > GeneroDTO
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            // GeneroCreacionDTO -> Genero
            CreateMap<GeneroCreacionDTO, Genero>();
        }
    }
}
