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
        public ComandaDBContext _context { get; set; }
        public PedidoCozinhaController(ComandaDBContext context)
        {
            _context = context;
        }

        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            var pedidos = _context.PedidosCozinha.ToList();
            return Results.Ok(pedidos);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult get(int id)
        {
            var pedido = _context.PedidosCozinha.FirstOrDefault(p => p.Id == id);
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
               
                SituaçãoPedido = pedido.SituaçãoPedido,
                ComandaItemId = pedido.ComandaItemId
            };
            _context.PedidosCozinha.Add(pedidonovo);
            _context.SaveChanges();
            return Results.Created($"/api/pedidocozinha/{pedidonovo.Id}", pedidonovo);
        }

        // PUT api/<PedidoCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest pedido)
        {
            var pedidoupdate = _context.PedidosCozinha.FirstOrDefault(p => p.Id == id);
            if (pedidoupdate is null)
            {
                return Results.NotFound();
            }
            pedidoupdate.SituaçãoPedido = pedido.SituaçãoPedido;
            pedido.ComandaItemId = pedido.ComandaItemId;
            _context.SaveChanges();
            return Results.Ok(pedidoupdate);

        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedido = _context.PedidosCozinha.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
            {
                return Results.NotFound($"pedido {id} nao encontrado");
            }
               _context.PedidosCozinha.Remove(pedido);
            var remove = _context.SaveChanges();
            if (remove > 0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
