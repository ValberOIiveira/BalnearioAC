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
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly Conexao _context;

        public RelatoriosController(Conexao context)
        {
            _context = context;
        }

        [HttpGet("reservations")]
        public async Task<IEnumerable<ReportReservation>> GetReportReservations()
        {
            return await _context.ReportReservations
                .Include(r => r.User)   
                .Include(r => r.Kiosk)  
                .Where(r => r.UserId != null)   
                .ToListAsync();
        }



        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<object>>> GetSalesReport(
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null)
        {
            var query = _context.Sales
                .Include(s => s.Employee)  // Carregar as informações do funcionário
                    .ThenInclude(e => e.User)  // Carregar o usuário associado ao funcionário (para acessar o nome)
                .Include(s => s.ItemSales) // Carregar os itens vendidos
                    .ThenInclude(its => its.Product)  // Carregar as informações do produto vendido
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(s => s.SaleDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.SaleDate <= endDate.Value);

            var sales = await query
                .Select(s => new
                {
                    s.Id,
                    SaleDate = s.SaleDate,
                    TotalPrice = s.TotalValue,
                    Employee = new
                    {
                        s.Employee.Id,
                        EmployeeName = s.Employee.User.Name  // Acessando o nome do funcionário através do User
                    },
                    Items = s.ItemSales.Select(its => new
                    {
                        its.ProductId,
                        ProductName = its.Product.Name,
                        its.Quantity,
                        its.Product.Price
                    })
                })
                .ToListAsync();

            return Ok(sales);
        }



        [HttpGet("user-activity")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserActivityReport(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var query = _context.LogUserActivities
                .Include(lua => lua.User)
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(lua => lua.ActivityDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(lua => lua.ActivityDate <= endDate.Value);

            var activities = await query
                .Select(lua => new
                {
                    lua.Id,
                    ActivityType = lua.ActivityType,
                    ActivityDate = lua.ActivityDate,
                    User = new { lua.User.Id, lua.User.Name, lua.User.Email }
                })
                .ToListAsync();

            return Ok(activities);
        }
    }
}