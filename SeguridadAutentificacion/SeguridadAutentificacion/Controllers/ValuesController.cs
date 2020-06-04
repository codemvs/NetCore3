using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using SeguridadAutentificacion.Service;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SeguridadAutentificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    //[EnableCors("PermitirApiRequest")] // abarcando todos los metodos del controlador
    public class ValuesController : ControllerBase
    {
        private readonly IDataProtector _protector;
        private readonly HashService _hashService;
        public ValuesController(IDataProtectionProvider protectionProvider, HashService hashService)
        {
            // Parte de la llave del algoritmo de encriptacion a utilizar, debe ser unico y de vez en cuando secreto
            string STRING_DE_PROPOSITO = "valor_unico_secreto"; 

            this._protector = protectionProvider.CreateProtector(STRING_DE_PROPOSITO);
            this._hashService = hashService;
        }
        // GET: api/Values
        //[EnableCors("PermitirApiRequest")] // abarca solo un meotodo especifico
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/values/hash
        [HttpGet("hash")]
        public ActionResult GetHash()
        {
            // Ejemplo hash 
            string textoPlano = "Hola soy un texto plano";
            var hashResult1 = _hashService.Hash(textoPlano).Has;
            var hashResult2 = _hashService.Hash(textoPlano).Has;

            return Ok(new {
                textoPlano,
                hashResult1,
                hashResult2
            });
        }
        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<string> Get(int id)
        {
            // Ejemplo texto cifrado
            object response = null;
            try
            {
                var protectorLimitadPorTiempo = _protector.ToTimeLimitedDataProtector();
                string textoPlano = "Ejemplo texto plano a encriptar";
                string textoCifrado = protectorLimitadPorTiempo.Protect(textoPlano, TimeSpan.FromSeconds(5)); //
                Thread.Sleep(6000);
                string textoDescifrado = protectorLimitadPorTiempo.Unprotect(textoCifrado);
                response = new
                {
                    textoPlano = textoPlano,
                    textoCifrado = textoCifrado,
                    textoDescifrado = textoDescifrado
                };
            }
            catch (Exception ex)
            {
                response = new { Error = ex.Message };
            }
            

            return Ok(response);
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
