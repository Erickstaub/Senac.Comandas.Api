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
        static List<CardapioItem> cardapios = new List<CardapioItem>()
        {
            new CardapioItem {
                Id = 1, Descricao= "Deliciosa coxinha de frango com catupiry", Preco = 5.50M, PossuiPreparo = true, Titulo = "Coxinha"
            },
            new CardapioItem {
                Id = 2, Descricao= "Deliciosa X-Tudo com catupiry", Preco = 25.50M, PossuiPreparo = true, Titulo = "X-Tudo"
            } };
        // GET: api/<CardapioItemController>
        [HttpGet]
        public IEnumerable<CardapioItem> Get()
        {
            return new CardapioItem[] {
            new CardapioItem {
            Id = 1,
            Titulo= "Coxinha",
            Descricao= "Deliciosa coxinha de frango com catupiry",
            Preco= 5.50M,
            PossuiPreparo= true
            },
            new CardapioItem {
            Id = 2,
            Titulo= "X-Tudo",
            Descricao= "Deliciosa X-Tudo com catupiry",
            Preco= 25.50M,
            PossuiPreparo= true
            }

            };
        }
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
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
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            cardapios.Add(newCardapio);
            return Results.Created($"/api/cardapioitem/{newCardapio.Id}", newCardapio);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapioPust)
        {
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound();
            }
            cardapio.Titulo = cardapioPust.Titulo;
            cardapio.Descricao = cardapioPust.Descricao;
            cardapio.Preco = cardapioPust.Preco;
            cardapio.PossuiPreparo = cardapioPust.PossuiPreparo;
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = cardapios
                .FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
            {
                return Results.NotFound($"caardapio {id} nao encontrado");
            }
            var remove = cardapios.Remove(cardapioItem);
            if(remove)
            {

            return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
