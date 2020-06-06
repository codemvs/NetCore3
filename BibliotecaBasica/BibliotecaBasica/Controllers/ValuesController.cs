using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BibliotecaBasica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values
        [HttpGet(Name = "ObtenerValores")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "ObtenerValor")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        [HttpPost(Name ="CrearValor")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}", Name = "ActializarValor")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}",Name = "BorrarValor")]
        public void Delete(int id)
        {
        }
    }
}
