using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
       public ComandaDBContext _contex { get; set; }
        public CardapioItemController(ComandaDBContext contex)
        {
            _contex = contex;
        }

        // GET: api/<CardapioItemController>
        [HttpGet]
        public IResult Get()
        {
            var cardapioitem = _contex.CardapioItens.ToList();
            return Results.Ok(cardapioitem);
        }
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var cardapio = _contex.CardapioItens.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if(cardapio.Titulo.Length < 3)
            {
                return Results.BadRequest("O título deve ter pelo menos 3 caracteres.");
            }
            if (cardapio.Descricao.Length < 5)
            {
                return Results.BadRequest("A descrição deve ter pelo menos 10 caracteres.");
            }
            if (cardapio.Preco <= 0)
            {
                return Results.BadRequest("O preço deve ser maior que zero.");
            }
            var newCardapio = new CardapioItem
            {
            
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            _contex.CardapioItens.Add(newCardapio);
            _contex.SaveChanges();
            return Results.Created($"/api/cardapioitem/{newCardapio.Id}", newCardapio);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapioPust)
        {
            var cardapio = _contex.CardapioItens.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound();
            }
            cardapio.Titulo = cardapioPust.Titulo;
            cardapio.Descricao = cardapioPust.Descricao;
            cardapio.Preco = cardapioPust.Preco;
            cardapio.PossuiPreparo = cardapioPust.PossuiPreparo;
            _contex.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = _contex.CardapioItens
                .FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
            {
                return Results.NotFound($"caardapio {id} nao encontrado");
            }
            _contex.CardapioItens.Remove(cardapioItem);
            var remove = _contex.SaveChanges();
            if(remove > 0)
            {

            return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
