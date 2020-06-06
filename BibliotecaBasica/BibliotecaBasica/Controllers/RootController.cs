using BibliotecaBasica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BibliotecaBasica.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController: ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public ActionResult<IEnumerable<Enlace>> Get()
        {
            List<Enlace> enlaces = new List<Enlace>();
            enlaces.Add(new Enlace(href:Url.Link("GetRoot", new { }),rel:"self", metodo:"GET"));
            enlaces.Add(new Enlace(href: Url.Link("ObtenerAutores", new { }), rel: "autores", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("CrearAutor", new { }), rel: "crear-autor", metodo: "POST"));
            enlaces.Add(new Enlace(href: Url.Link("ObtenerValores", new { }), rel: "valores", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("CrearValores", new { }), rel: "crear-valores", metodo: "POST"));
            return enlaces;
        }
    }
}
