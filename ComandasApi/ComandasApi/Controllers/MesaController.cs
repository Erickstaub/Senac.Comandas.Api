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
        public ComandaDBContext _context { get; set; }
       public MesaController(ComandaDBContext context)
        {
            _context = context;
        }
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            var mesas = _context.Mesas.ToList();
            return Results.Ok(mesas);
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
           var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
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
           
                SituaçãoMesa = mesapost.SituaçãoMesa,
                NumeroMesa = mesapost.NumeroMesa
            };
            _context.Mesas.Add(mesanova);
            _context.SaveChanges();
            return Results.Created($"/api/mesa/{mesanova.Id}", mesanova);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaput)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if(mesaput.NumeroMesa <= 0)
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
            _context.SaveChanges();
             return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound($"mesa {id} nao encontrada");
            }
           _context.Mesas.Remove(mesa);
            var remove = _context.SaveChanges();
            if (remove > 0)
            {
                return Results.NoContent();
            }
            return Results.StatusCode(500);
        }
    }
}
