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

        // Método para obter todos os visitantes com os dados do usuário
        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetVisitors()
        {
            return await _context.Visitors
                                .Include(v => v.User)  
                                .ToListAsync();
        }


        // Método para obter um visitante específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitorById(int id)
        {
            var visitor = await _context.Visitors.Include(v => v.User).FirstOrDefaultAsync(v => v.Id == id);
            if (visitor == null)
                return NotFound("Visitante não encontrado");

            return Ok(visitor);
        }

        // Método para criar um novo visitante
        [HttpPost]
        public async Task<IActionResult> CreateVisitor([FromBody] Visitor visitor)
        {
            if (visitor == null)
            {
                return BadRequest("Dados inválidos");
            }

            // Verificar se o usuário existe
            var userExists = await _context.Users.AnyAsync(u => u.Id == visitor.IdUser);
            if (!userExists)
            {
                return BadRequest("Usuário não encontrado");
            }

            try
            {
                _context.Visitors.Add(visitor);
                await _context.SaveChangesAsync();
                return Ok(visitor);
            }
            catch (Exception ex)
            {
                // Log do erro no console ou log para o backend
                Console.WriteLine($"Erro ao salvar visitante: {ex.Message}");
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }


        // Método para atualizar um visitante existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(int id, Visitor visitor)
        {
            if (id != visitor.Id)
                return BadRequest("ID da URL não bate com o ID do visitante.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _context.Users.AnyAsync(u => u.Id == visitor.IdUser);
            if (!userExists)
                return NotFound("Usuário não encontrado.");

            _context.Entry(visitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


       // Método para excluir um visitante
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
                return NotFound("Visitante não encontrado");

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return Ok("Visitante excluído com sucesso");
        }

        // <-- Adiciona isso aqui no final:
        private bool VisitorExists(int id)
        {
            return _context.Visitors.Any(e => e.Id == id);
        }
    }
}
