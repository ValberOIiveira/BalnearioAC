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
    public class UserController : ControllerBase
    {
        private readonly Conexao _context;

        public UserController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> getUser()
        {
            return await _context.Users.ToListAsync();
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
            existingUser.Age = user.Age;
            existingUser.Passwd = user.Passwd;
            existingUser.Id_user_type = user.Id_user_type;

            await _context.SaveChangesAsync();

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