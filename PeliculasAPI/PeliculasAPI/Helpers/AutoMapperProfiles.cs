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

            // Actor < - > ActorDTO
            CreateMap<Actor, ActorDTO>().ReverseMap();
            //ActorCreacionDTO - > Actor
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x=>x.Foto, option => option.Ignore()); // ignorar el mapeo de la foto            
            //ActorCreacionDTO < - > Actor
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            // Pelicula < - > PeliculaDTO
            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            //PeliculaCreacionDTO - > Pelicula
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, option => option.Ignore()); // ignorar el mapeo de la foto(poster)            
            //PeliculaPatchDTO < - > Pelicula
            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();
        }
    }
}
