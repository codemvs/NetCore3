using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System.Collections.Generic;

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

            // SalaDeCine < - > SalaDeCineDTO
            CreateMap<SalaDeCine, SalaDeCineDTO>().ReverseMap();
            // SalaDeCineCreacionDTO -> SalaDeCine
            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>();

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
                .ForMember(x => x.Poster, option => option.Ignore()) // ignorar el mapeo de la foto(poster)            
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros)) // Mapear pelicuas generos
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculasAutores)); // Peliculaa Actores

            //Pelicula -> PeliculaDetalleDTO
            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));


            //PeliculaPatchDTO < - > Pelicula
            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();
        }
        private List<ActorPeliculaDetalleDTO> MapPeliculasActores( Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();
            if (pelicula.PeliculasActores == null)
            {
                return resultado;
            }
            foreach (var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO()
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    NombrePersona = actorPelicula.Actor.Nombre
                });
            }
            return resultado;
        }
        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();
            if (pelicula.PeliculasGeneros == null)
            {
                return resultado;
            }

            foreach (var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO() { Id = generoPelicula.GeneroId, Nombre = generoPelicula.Genero.Nombre });
            }

            return resultado;
        }
        private IEnumerable<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCreacionDTO.GenerosIDs == null) return resultado;

            foreach(var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }
        private IEnumerable<PeliculasActores> MapPeliculasAutores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculaCreacionDTO.Actores == null) return resultado;

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje = actor.Personaje});
            }
            return resultado;
        }
    }
}
