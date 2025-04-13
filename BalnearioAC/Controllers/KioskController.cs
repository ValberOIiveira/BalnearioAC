using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalnearioAC.Models;
using BalnearioAC.Database;

namespace BalnearioAC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KioskController : ControllerBase
    {
        private readonly Conexao _context;

        public KioskController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kiosk>>> GetKiosks()
        {
            return await _context.Kiosks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kiosk>> GetKiosk(int id)
        {
            var kiosk = await _context.Kiosks.FindAsync(id);

            if (kiosk == null)
                return NotFound();

            return Ok(kiosk);
        }

        [HttpPost]
        public async Task<ActionResult<Kiosk>> CreateKiosk(Kiosk kiosk)
        {
            _context.Kiosks.Add(kiosk);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKiosk), new { id = kiosk.Id }, kiosk);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKiosk(int id, Kiosk kiosk)
        {
            if (id != kiosk.Id)
                return BadRequest();

            _context.Entry(kiosk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Kiosks.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKiosk(int id)
        {
            var kiosk = await _context.Kiosks.FindAsync(id);
            if (kiosk == null)
                return NotFound();

            _context.Kiosks.Remove(kiosk);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
