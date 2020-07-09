
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> Get()
        {
            var entidades =await context.Actores.ToListAsync();
            return mapper.Map<List<ActorDTO>>(entidades);
        }
        [HttpGet("{id}", Name ="obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync( x=>x.Id==id);
            if(actor is null)
            {
                return NotFound();
            }
            return mapper.Map<ActorDTO>(actor);
            
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if(actorCreacionDTO.Foto != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actor.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, actorCreacionDTO.Foto.ContentType);
                }
            }

            context.Add(actor);
            await context.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(actor);

            return new CreatedAtRouteResult("obtenerActor", new { id = actor.Id }, actorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if(actorDB == null)
            {
                return NotFound();
            }

            // obtiene los campos de actorCreacionDTO y los mapea a actorDB
            actorDB = mapper.Map(actorCreacionDTO, actorDB);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actorDB.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,
                        actorDB.Foto ,actorCreacionDTO.Foto.ContentType);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Actores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Actores.Remove(new Actor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
