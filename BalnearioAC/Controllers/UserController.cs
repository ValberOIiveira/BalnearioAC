using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BalnearioAC.Database;
using BalnearioAC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BalnearioAC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Conexao _context;
        private readonly TimeZoneInfo _brasilTimeZone;

        public UserController(Conexao context)
        {
            _context = context;
            _brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => 
            {
                if (u.Age.HasValue)
                {
                    u.Age = TimeZoneInfo.ConvertTimeFromUtc(u.Age.Value, _brasilTimeZone);
                }
                return u;
            }).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Usuário não pode ser nulo");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Converte de volta para mostrar no response
            if (user.Age.HasValue)
            {
                user.Age = TimeZoneInfo.ConvertTimeFromUtc(user.Age.Value, _brasilTimeZone);
            }

            return user.Id > 0 ? Ok(user) : BadRequest("Erro ao cadastrar usuário");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("Usuário não pode ser nulo ou ID inválido");
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("Usuário não encontrado");
            }

            existingUser.Name = user.Name;
            existingUser.Cpf = user.Cpf;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.Age = user.Age; // Já é convertido automaticamente pelo model
            existingUser.Passwd = user.Passwd;
                

            await _context.SaveChangesAsync();

            // Converte de volta para mostrar no response
            if (existingUser.Age.HasValue)
            {
                existingUser.Age = TimeZoneInfo.ConvertTimeFromUtc(existingUser.Age.Value, _brasilTimeZone);
            }

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário excluído com sucesso");
        }
    }
}