
using BibliotecaBasica.Context;
using BibliotecaBasica.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaBasica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: /api/libros
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros
                    .Include(libro => libro.Autor)
                    .ToList();

        }
        // GET: /api/libros/1
        [HttpGet("{id}", Name = "GetLibroById")]
        public ActionResult<Libro> Get(int id){
            Libro libro = context.Libros                            
                            .FirstOrDefault(libro => libro.Id == id);
            if(libro == null)
            {
                return NotFound();
            }
            return libro;
                            
        }
        // POST: /api/libros
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);            
            context.SaveChanges();
            return new CreatedAtRouteResult("GetLibroById", new { id = libro.Id }, libro);
        }
        // PUT: /api/libros/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {

            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        
        // DELETE: /api/libros/2
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libros.FirstOrDefault((libro)=> libro.Id == id);
            if(libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}