using ComandasApi.DTOs;
using ComandasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CardapioItemController : ControllerBase
    {
        // get contex
        public ComandaDBContext _contex { get; set; }
        //construtor

        public CardapioItemController(ComandaDBContext contex)
        {
            _contex = contex;
        }

        // GET: api/<CardapioItemController>
        [HttpGet]
        public IResult Get()
        {
            // listar todos os cardapio itens
            var cardapioitem = _contex.CardapioItens.Include(c => c.CategoriaCardapio).ToList();
            return Results.Ok(cardapioitem);
        }
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            // listar cardapio item por id  
            var cardapio = _contex
                .CardapioItens
                .Include(ci => ci.CategoriaCardapio)
                .FirstOrDefault(c => c.Id == id);
            // select * from CardapioItens inner join categoriacardapio in cc.id = c.categoriacardapioid where Id = 1
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
            // validações
            if (cardapio.Titulo.Length < 3)
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
            // criar novo item do cardapio
            //validacao da categoria se for preenchida
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _contex.CategoriaCardapio
                    .FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId.Value);
                if (categoria is null)
                {
                    return Results.BadRequest("Categoria de cardápio inválida.");
                }
            }
            var newCardapio = new CardapioItem
            {
            
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
                CategoriaCardapioId = cardapio.CategoriaCardapioId
            };
            _contex.CardapioItens.Add(newCardapio);
            _contex.SaveChanges();
            return Results.Created($"/api/cardapioitem/{newCardapio.Id}", newCardapio);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapioPust)
        {
            // atualizar item do cardapio
            var cardapio = _contex.CardapioItens.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound();
            }
            // validações
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _contex.CategoriaCardapio.FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId);
                //se retorno e nulo
                if (categoria is null)
                {
                    return Results.BadRequest("Categoria de cardápio inválida.");
                }
            }
            cardapio.Titulo = cardapioPust.Titulo;
            cardapio.Descricao = cardapioPust.Descricao;
            cardapio.Preco = cardapioPust.Preco;
            cardapio.PossuiPreparo = cardapioPust.PossuiPreparo;
            cardapio.CategoriaCardapioId = cardapio.CategoriaCardapioId;
            _contex.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            // deletar item do cardapio
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
