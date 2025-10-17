using ComandasApi.DTOs;
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
        public IResult Post([FromBody] PedidoCozinhaCreateRequest pedido)
        {
            if (pedido.ComandaItemId <= 0)
            {
                return Results.BadRequest("O ID do item da comanda deve ser maior que zero.");
            }
            if(pedido.SituaçãoPedido < 0 || pedido.SituaçãoPedido > 2)
            {
                return Results.BadRequest("A situação do pedido é inválida.");
            }

            var pedidonovo = new PedidoCozinha
            {
                Id = pedidos.Count + 1,
                SituaçãoPedido = pedido.SituaçãoPedido,
                ComandaItemId = pedido.ComandaItemId
            };
            pedidos.Add(pedidonovo);
            return Results.Created($"/api/pedidocozinha/{pedidonovo.Id}", pedidonovo);
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedido)
        {
            var pedidoupdate = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedidoupdate is null)
            {
                return Results.NotFound();
            }
            pedidoupdate.SituaçãoPedido = pedido.SituaçãoPedido;
            pedido.ComandaItemId = pedido.ComandaItemId;
            return Results.Ok(pedidoupdate);

        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
            {
                return Results.NotFound($"pedido {id} nao encontrado");
            }
               var remove =  pedidos.Remove(pedido);
            if (remove)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
