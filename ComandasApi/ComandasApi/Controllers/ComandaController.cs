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
       public ComandaDBContext _context { get; set; }
        public ComandaController(ComandaDBContext context)
        {
            _context = context;
        }

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            var comandas = _context.Comandas.ToList();
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
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
             
                ClienteNome = comandacreate.nomeCliente,
                MesaId = comandacreate.numeroMesa,
            };
            var comandaItens = new List<ComandaItem>();
            foreach (int cardaItens in comandacreate.CardapioItensId)
            {
                var comandaItem = new ComandaItem
                {

                    CardapioItemId = cardaItens,
                    Comanda = novaComanda
                };
                comandaItens.Add(comandaItem);

                // criar o pedido de cozinha e o item de acordo com o cadastro do cardapio possuipreparo
                var cardapioitem = _context.CardapioItens.FirstOrDefault(c => c.Id == cardaItens);
                if (cardapioitem!.PossuiPreparo == true)
                {
                    var pedido = new PedidoCozinha
                    {
                        Comanda = novaComanda,

                    };
                    var pedidoItem = new PedidoCozinhaItem
                    {
                        ComandaItem = comandaItem,
                        PedidoCozinha = pedido
                    };
                    _context.PedidosCozinha.Add(pedido);
                    _context.PedidosCozinhaItens.Add(pedidoItem);
                }
            }
            novaComanda.Itens = comandaItens;
          _context.Comandas.Add(novaComanda);
            _context.SaveChanges();
            var response = new ComandaCreateResponse
            {
                Id = novaComanda.Id,
                NomeCliente = novaComanda.ClienteNome,
                NumeroMesa = novaComanda.MesaId,
                Itens = novaComanda.Itens.Select(i => new ComandaItemResponse
                {
                    Id = i.Id,
                    Titulo = _context.CardapioItens.
                    First(c => c.Id == i.CardapioItemId).Titulo
                }).ToList()

            };

            return Results.Created($"/api/comanda/{novaComanda.Id}", response);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comanda)
        {
            var comandaAtual = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comandaAtual is null)
            {
                return Results.NotFound();
            }
           
            comandaAtual.ClienteNome = comanda.nomeCliente;
            comandaAtual.SituaçãoComanda = comanda.numeroMesa;

            //itens
            foreach(var item in comanda.Itens)
            {
                //se id for informado e remover for verdadeiro
                if(item.Id >0 && item.Remove == true)
                {

                    //remover item
                    RemoverItemComanda(item.Id);
                }
                // se cardapioid foi informando
                if(item.CardapioItensId > 0)
                {
                    //adicionar item
                    InserirItemComanda(comandaAtual, item.CardapioItensId);
                }
            }

            _context.SaveChanges();
            return Results.NoContent();

        }

        private void InserirItemComanda(Comanda comanda, int cardapioItensId)
        {
            _context.ComandaItens.Add(new ComandaItem
            {
                CardapioItemId = cardapioItensId,
                Comanda = comanda
            });
        }

        private void RemoverItemComanda(int id)
        {
            //consulta o item
          var comandaItem = _context.ComandaItens.FirstOrDefault(ci => ci.Id == id);
            if (comandaItem is not null)
            {
                _context.ComandaItens.Remove(comandaItem);
            }
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
            {
                return Results.NotFound($"comanda {id} nao encontrada");
            }
            _context.Comandas.Remove(comanda);
            var remove = _context.SaveChanges();
            if(remove > 0)
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
