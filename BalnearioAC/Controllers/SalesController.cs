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
    public class SalesController : ControllerBase
    {
        private readonly Conexao _context;

        public SalesController(Conexao context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> PostSale([FromBody] Sale sale)
        {
            if (sale == null)
            {
                return BadRequest("A venda não pode ser nula");
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return sale.Id > 0 ? Ok(sale) : BadRequest("Erro ao cadastrar venda");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, [FromBody] Sale sale)
        {
            if (sale == null || id != sale.Id)
            {
                return BadRequest("A venda não pode ser nula ou ID inválido");
            }

            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale == null)
            {
                return NotFound("Venda não encontrada");
            }

            existingSale.Id = sale.Id;
            existingSale.EmployeeId = sale.EmployeeId;
            existingSale.SaleDate = sale.SaleDate;
            existingSale.TotalValue = sale.TotalValue;

            await _context.SaveChangesAsync();

            return Ok(existingSale);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound("Venda não encontrada");
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return Ok("Venda excluída com sucesso");
        }
    }
}