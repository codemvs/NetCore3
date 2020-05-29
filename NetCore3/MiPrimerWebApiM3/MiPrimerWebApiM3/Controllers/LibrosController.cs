
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MiPrimerWebApiM3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include( libro => libro.Autor ).ToList();
        }
        [HttpGet("{id}", Name ="ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.Include(libro => libro.Autor)
                                      .FirstOrDefault(libro => libro.Id == id);
            if(libro == null)
            {
                return NotFound();
            }
            return libro;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Libro libro)
        {
            if(id != libro.Id)
            {
                return BadRequest();
            }
            context.Libros.Update(libro);
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.Include(libro => libro.Autor)
                                      .FirstOrDefault(libro => libro.Id == id);
            if(libro == null)
            {
                return BadRequest();
            }
            return libro;
        }
    }
}
