using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        public List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa {
                Id = 1, SituaçãoMesa = (int)SituacaoMesa.Livre , NumeroMesa = 1
            },
            new Mesa {
                Id = 2, SituaçãoMesa = (int)SituacaoMesa.Ocupada, NumeroMesa = 2
            } };
        // GET: api/<MesaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
           var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(mesa);
        }

        // POST api/<MesaController>
        [HttpPost]
        public void Post([FromBody] MesaCreatRequest mesapost)
        {

        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MesaUpdateRequest mesaput)
        {
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
