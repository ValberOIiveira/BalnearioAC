using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BalnearioAC.Database;
using BalnearioAC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalnearioAC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly Conexao _context;

        public VisitorController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetVisitors()
        {
            return await _context.Visitors.ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> PostVisitor([FromBody] Visitor visitor)
        {
            if (visitor == null)
            {
                return BadRequest("Visitante não pode ser nulo");
            }

            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();

            return visitor.Id > 0 ? Ok(visitor) : BadRequest("Erro ao cadastrar visitante");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(int id, [FromBody] Visitor visitor)
        {
            if (visitor == null || id != visitor.Id)
            {
                return BadRequest("Visitante não pode ser nulo ou ID inválido");
            }

            var existingVisitor = await _context.Visitors.FindAsync(id);
            if (existingVisitor == null)
            {
                return NotFound("Visitante não encontrado");
            }

            existingVisitor.Name = visitor.Name;
            existingVisitor.Cpf = visitor.Cpf;
            existingVisitor.Age = visitor.Age;
            existingVisitor.IdUser = visitor.IdUser;

            await _context.SaveChangesAsync();

            return Ok(existingVisitor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound("Visitante não encontrado");
            }

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return Ok("Visitante excluído com sucesso");
        }
    }
}