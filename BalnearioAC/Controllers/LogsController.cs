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
    public class LogsController : ControllerBase
    {
        private readonly Conexao _context;
        public LogsController(Conexao context)
        {
            _context = context;
        }
       [HttpGet("reservations")]
    public async Task<ActionResult<IEnumerable<LogReservation>>> GetLogReservations()
    {
        var logs = await _context.LogReservations
        .Include(lr => lr.Reservation)
        .Include(lr => lr.User)
        .ToListAsync();
        return Ok(logs);
    }
        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<LogSale>>> GetLogSales()
        {
            var logs = await _context.LogSales
            .Include(ls => ls.Sale)
            .Include(ls => ls.User) 
            .ToListAsync();
                return Ok(logs);
        }
        [HttpGet("user-activity")]
        public async Task<ActionResult<IEnumerable<LogUserActivity>>> GetLogUserActivity()
        {
            var logs = await _context.LogUserActivities
                .Include(lua => lua.User)
                .ToListAsync();
            return Ok(logs);
        }
    }
}