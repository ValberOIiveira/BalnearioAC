using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalnearioAC.Database;
using BalnearioAC.Models;

namespace BalnearioAC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Conexao _context;

        public LoginController(Conexao context)
        {
            _context = context;
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request) 
        {
            
            var usuario = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => 
                    u.Email == request.Email && 
                    u.Passwd == request.Password);

            
            if (usuario == null)
                return Unauthorized("Email ou senha incorretos");

            return Ok(new { 
                Success = true,
                Message = "Login realizado com sucesso"
            });
        }
    }

    
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}