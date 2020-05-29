using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNetCoreStoredProcedureDemo.Data;
using ApiNetCoreStoredProcedureDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetCoreStoredProcedureDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValuesRepository _repository;

        public ValuesController(ValuesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: /api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            var values = await _repository.GetAll();
            return values.ToList();
        }

        // GET: /api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        // Post: /api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Value value)
        {
            await _repository.Insert(value);
            return Ok();
        }
        // Delete: /api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteById(id);
            return NoContent();
        }

    }
}