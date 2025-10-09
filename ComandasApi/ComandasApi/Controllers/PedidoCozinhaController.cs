using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        List<PedidoCozinha> pedidos = new List<PedidoCozinha>()
        {
            new PedidoCozinha {
                Id = 1, SituaçãoPedido = (int)SituaçãoPedido.Pendente , ComandaItemId= 1, 
            },
            new PedidoCozinha {
                Id = 2, SituaçãoPedido = (int)SituaçãoPedido.Pronto, ComandaItemId = 2
            } };
        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult get(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(pedido);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
