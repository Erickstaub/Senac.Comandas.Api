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
        public IResult Post([FromBody] MesaCreatRequest mesapost)
        {
            if (mesapost.NumeroMesa <= 0)
            {
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            }
            var mesanova = new Mesa
            {
                Id = mesas.Count + 1,
                SituaçãoMesa = mesapost.SituaçãoMesa,
                NumeroMesa = mesapost.NumeroMesa
            };
            mesas.Add(mesanova);
            return Results.Created($"/api/mesa/{mesanova.Id}", mesanova);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaput)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if(mesaput.NumeroMesa <= 0 || mesaput.SituaçãoMesa <=0)
            {
                return Results.BadRequest("O número deve ser maior que zero.");
            }
            if (mesaput.SituaçãoMesa > 2)
            {
                return Results.BadRequest("Situação inválida. Use 0 para Livre e 1 para Ocupada e 2 para reservada.");
            }
            if (mesa is null)
            {
              return   Results.NotFound($"Mesa {id} nao encontrada");
            }
            mesa.SituaçãoMesa = mesaput.SituaçãoMesa;
            mesa.NumeroMesa = mesaput.NumeroMesa;
             return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound($"mesa {id} nao encontrada");
            }
            var remove = mesas.Remove(mesa);
            if(remove)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
