using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalnearioAC.Database;
using BalnearioAC.Models;

namespace BalnearioAC.Controllers
{
    //Mudar o tipo da data no banco de dados para timestamp
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly Conexao _context;

        public ReservationController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _context.Reservations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> Get(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();
            return reservation;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Post([FromBody] Reservation reservation)
        {
            // Verificar se o visitante existe
            var visitor = await _context.Visitors.FindAsync(reservation.VisitorId);
            if (visitor == null)
            {
                return BadRequest("Visitor not found");
            }

            // Verificar se o quiosque existe
            var kiosk = await _context.Kiosks.FindAsync(reservation.KioskId);
            if (kiosk == null)
            {
                return BadRequest("Kiosk not found");
            }

            // Se ambos o visitante e o quiosque forem encontrados, cria a reserva
            reservation.StartDate = DateTime.SpecifyKind(reservation.StartDate, DateTimeKind.Utc);
            reservation.EndDate = DateTime.SpecifyKind(reservation.EndDate, DateTimeKind.Utc);

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Retorna a reserva criada
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Reservation>> Put(int id, [FromBody] Reservation reservation)
        {
            var existente = await _context.Reservations.FindAsync(id);
            if (existente == null) return NotFound();

            existente.StartDate = reservation.StartDate;
            existente.EndDate = reservation.EndDate;
            existente.VisitorId = reservation.VisitorId;
            existente.KioskId = reservation.KioskId;

            await _context.SaveChangesAsync();
            return existente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> Delete(int id)
        {
            var existente = await _context.Reservations.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Reservations.Remove(existente);
            await _context.SaveChangesAsync();
            return existente;
        }
    }
}
