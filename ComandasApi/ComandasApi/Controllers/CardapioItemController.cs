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
        public List<CardapioItem> cardapios = new List<CardapioItem>()
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
        public void Post([FromBody] CardapioItemCreateRequest cardapio)
        {

        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CardapioItemUpdateRequest cardapioPuSt)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
