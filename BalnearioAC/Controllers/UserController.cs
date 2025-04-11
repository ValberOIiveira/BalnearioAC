using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using BalnearioAC.Database;
using BalnearioAC.Models;

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
        public async Task<IEnumerable<User>> Get() 
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user) 
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        { 
            var existente = await _context.Users.FindAsync(id); 
            if (existente == null) return NotFound();   

            existente.Name = user.Name;
            existente.Cpf = user.Cpf;
            existente.Email = user.Email;
            existente.Phone = user.Phone;
            existente.Age = user.Age;
            existente.Passwd = user.Passwd;
            existente.Id_user_type = user.Id_user_type;
            await _context.SaveChangesAsync();
            return existente;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id) 
        {
            var existente = await _context.Users.FindAsync(id);
            if (existente == null) return NotFound();
            _context.Users.Remove(existente);
            await _context.SaveChangesAsync();
            return existente;
        }
        
    }
}