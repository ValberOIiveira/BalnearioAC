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
    public class UserTypeController : ControllerBase
    {
        private readonly Conexao _context;

        public UserTypeController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<UserType>> getUserType() 
        {
            return await _context.UserTypes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserType>> postUserType([FromBody] UserType userType)
        {
            if (userType == null)
            {
                return BadRequest("Usuário nao pode ser nulo");
            }

            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();
            return userType;
        }
    }
}