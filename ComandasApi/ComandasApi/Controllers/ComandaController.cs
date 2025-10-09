using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        List <Comanda> comandas = new List<Comanda>()
        {
            new Comanda {
                Id = 1, SituaçãoComanda = (int)SituaçãoComanda.Aberta , MesaId = 1, ClienteNome = "João"
            },
            new Comanda {
                Id = 2, SituaçãoComanda = (int)SituaçãoComanda.Fechada, MesaId = 2, ClienteNome = "Maria"
            } };
        // GET: api/<ComandaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandacreate)
        {
            if(comandacreate.nomeCliente.Length < 3)
               return Results.BadRequest("O nome do cliente deve ter pelo menos 3 caracteres.");
            if (comandacreate.CardapioItensId.Length == 0)
                return Results.BadRequest("A comanda deve ter pelo menos um item.");
            if (comandacreate.numeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            var novaComanda = new Comanda
            {
                Id = comandas.Count + 1,
                ClienteNome = comandacreate.nomeCliente,
                MesaId = comandacreate.numeroMesa,
        

            };
            comandas.Add(novaComanda);
            return Results.Created($"/api/comanda/{novaComanda.Id}", novaComanda);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ComandaUpdateRequest comanda)
        {
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
