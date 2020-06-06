using AutoMapper;
using BibliotecaBasica.Context;
using BibliotecaBasica.Entities;
using BibliotecaBasica.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaBasica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public AutoresController(ApplicationDbContext context,
                                 IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        // GET: /api/autores
        [HttpGet(Name = "ObtenerAutores")]
        public async Task<IActionResult> Get(bool incluirEnlacesHATEOAS = false) {

            var autores = await context.Autores.ToListAsync();
            
            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);

            var resultado = new ColeccionDeRecursos<AutorDTO>(autoresDTO);

            if (incluirEnlacesHATEOAS)
            {
                autoresDTO.ForEach(a => GenerarEnlaces(a));
                resultado.Enlaces.Add(new Enlace(href:Url.Link("ObtenerAutores",new { }), rel: "self", metodo: "GET"));
                resultado.Enlaces.Add(new Enlace(href: Url.Link("CrearAutor", new { }), rel: "CreateAuthor", metodo: "POST"));
                return Ok(resultado);
            }
            return Ok(autoresDTO);
        }
        // GET: /api/autores/2
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context
                        .Autores
                        .FirstOrDefaultAsync(autor => autor.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);
            GenerarEnlaces(autorDTO);
            return autorDTO;
        }

        /**
         * Metodo se utiliza para generar enlaces del endpoint que reciben un parametro de entrada
         */
        private void GenerarEnlaces(AutorDTO autor)
        {
            autor.Enlaces.Add(new Enlace(href:Url.Link("ObtenerAutor",new {id = autor.Id }),rel:"self",metodo:"GET"));
            autor.Enlaces.Add(new Enlace(href: Url.Link("ActualizarAutor", new { id = autor.Id }), rel: "update-autor", metodo: "PUT"));
            autor.Enlaces.Add(new Enlace(href: Url.Link("BorrarAutor", new { id = autor.Id }), rel: "delete-autor", metodo: "DELETE"));
        }

        // Post: /api/autores        
        [HttpPost(Name ="CrearAutor")]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            
            context.Autores.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        // Put: /api/autores/1
        [HttpPut("{id}", Name = "ActualizarAutor")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;

            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        /**
         *patchDocument = [{
	            "op":"replace", // operación
	            "path":"/nombre", // atributo a modificar
	            "value":"Patch" // Nuevo valor
            }]
         */
        // Patch: /api/autores/1
        [HttpPatch("{id}", Name = "ActualizarParcialmenteAutor")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument) {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var autorBD = await context.Autores.FirstOrDefaultAsync(autor => autor.Id == id);
            if(autorBD == null)
            {
                return NotFound();
            }

            // Mapear autor
            var autorDTO = mapper.Map<AutorCreacionDTO>(autorBD);

            // Validar si el modelo es valido
            patchDocument.ApplyTo(autorDTO, ModelState);

            mapper.Map(autorDTO, autorBD);

            var isValid = TryValidateModel(autorBD);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            // Realizar cambios a la bd
            await context.SaveChangesAsync();
            return NoContent();

        }

        // Delete: /api/autores/1
        [HttpDelete("{id}", Name = "BorrarAutor")]
        public async Task<ActionResult> Delete(int id)
        {
            var autorId =await context.Autores.Select(autor => autor.Id).FirstOrDefaultAsync(autorId => autorId == id);
            if(autorId == default(int))
            {
                return NotFound();
            }
            context.Autores.Remove(new Autor {Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}