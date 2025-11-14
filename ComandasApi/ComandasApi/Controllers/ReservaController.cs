using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComandasApi;
using ComandasApi.Models;

namespace ComandasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ComandaDBContext _context;

        public ReservaController(ComandaDBContext context)
        {
            _context = context;
        }

        // GET: api/Reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // GET: api/Reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<reserva>> Getreserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reserva/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putreserva(int id, reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            var entry=_context.Entry(reserva).State = EntityState.Modified;
            var novaMesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (novaMesa is null)
            {
                return BadRequest("Mesa não encontrada.");
            }
            novaMesa.SituaçãoMesa = (int)SituacaoMesa.Reservada;
            var reservaOriginal = await _context.Reservas.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            var numeroMesaOriginal = reservaOriginal!.NumeroMesa;
            var mesaOriginal = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesaOriginal);
            mesaOriginal!.SituaçãoMesa = (int)SituacaoMesa.Livre;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reserva
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<reserva>> Postreserva(reserva reserva)
        {
            _context.Reservas.Add(reserva);
            // atualizar o status da mesa para "Reservada"
            //consultar a mesa por id
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if(mesa is null)
            {
                return BadRequest("Mesa não encontrada.");
            }
            if(mesa is not null)
            {
                if(mesa.SituaçãoMesa != (int)SituacaoMesa.Livre)
                {
                    return BadRequest("Mesa não está disponível para reserva.");
                }
                mesa.SituaçãoMesa = (int)SituacaoMesa.Reservada;
           
            }


            await _context.SaveChangesAsync();

            return CreatedAtAction("Getreserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletereserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("Reserva nao encontrada");
            }
            
            //atualizar a mesa para livre
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if(mesa is null)
            {
                return BadRequest("Mesa não encontrada.");

            }
            mesa.SituaçãoMesa = (int)SituacaoMesa.Livre;
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool reservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
