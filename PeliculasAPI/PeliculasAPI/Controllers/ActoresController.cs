
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context,
            IMapper mapper)
        {
            
            this.context = context;
            this.mapper = mapper;
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
        public async Task<ActionResult> Post([FromBody] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(actor);

            return new CreatedAtRouteResult("obtenerActor", new { id = actor.Id }, actorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreacionDTO actorCreacionDTO)
        {
            
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            actor.Id = id;
            context.Entry(actor).State = EntityState.Modified;
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
