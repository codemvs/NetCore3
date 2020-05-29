using Microsoft.AspNetCore.Mvc;
using MiPrimerWebApiM3.Contexts;
using System.Collections.Generic;
using MiPrimerWebApiM3.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MiPrimerWebApiM3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }
        //GET /listado
        [HttpGet("/listado")]
        //GET /api/autores/listado
        [HttpGet("listado")]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores
                .Include(autor => autor.Libros)
                .ToList();
        }
        //GET /api/autores/3 ó
        // GET /api/autores/3/lupita
        [HttpGet("{id}/{nombre?}", Name ="ObtenerAutor")]
        public async Task<ActionResult<Autor>> Get(int id, [BindRequired]string nombre) // Método asincrono
        {

            var autor = await context.Autores
                .Include(autor => autor.Libros)
                .FirstOrDefaultAsync(autor => autor.Id == id);
            if(autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        //GET /api/autores/primer
        [HttpGet("Primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.FirstOrDefault();
        }
        //POST /api/autores
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            //TryValidateModel(autor); Revalidar model
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id}, autor);
        }
        //PUT /api/autores/3
        [HttpPut("{id}")] 
        public ActionResult Put(int id, [FromBody] Autor autor) 
        {
            if(id != autor.Id)
            {
                return BadRequest();
            }
            context.Entry(autor).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        //DELETE api/autores/8
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(autor => autor.Id == id);
            if(autor == null)
            {
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}
