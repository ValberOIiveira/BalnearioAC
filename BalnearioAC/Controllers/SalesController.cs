using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BalnearioAC.Database;
using BalnearioAC.Models;
using BalnearioAC.DTOs;
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
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Employee)
                    .ThenInclude(e => e.User)
                .Include(s => s.ItemSales)
                    .ThenInclude(i => i.Product)
                .Select(s => new SaleDTO
                {
                    Id = s.Id,
                    SaleDate = s.SaleDate,
                    TotalValue = s.TotalValue,
                    EmployeeName = s.Employee != null && s.Employee.User != null
                        ? s.Employee.User.Name
                        : "Funcionário não encontrado",
                    ItemSales = s.ItemSales.Select(i => new ItemSaleDTO
                    {
                        Id = i.Id,
                        ProductId = i.ProductId ?? 0,
                        ProductName = i.Product != null ? i.Product.Name : "Produto não encontrado",
                        Quantity = i.Qtd,
                        UnitPrice = i.Product != null ? i.Product.Price : 0
                    }).ToList()
                })
                .ToListAsync();

            return Ok(sales);
        }





        [HttpPost]
        public async Task<IActionResult> PostSale([FromBody] Sale sale)
        {
            if (sale == null)
                return BadRequest("A venda não pode ser nula");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Primeiro: Cria a venda
                var novaVenda = new Sale
                {
                    EmployeeId = sale.EmployeeId,
                    SaleDate = sale.SaleDate,
                    TotalValue = sale.TotalValue
                };

                _context.Sales.Add(novaVenda);
                await _context.SaveChangesAsync(); // Gera o ID da venda aqui!

                // Depois: Cria os itens vinculados à venda
                if (sale.ItemSales != null && sale.ItemSales.Any())
                {
                    foreach (var item in sale.ItemSales)
                    {
                        var novoItem = new ItemSale
                        {
                            SaleId = novaVenda.Id, // Usa o ID gerado
                            ProductId = item.ProductId,
                            Qtd = item.Qtd
                        };

                        _context.ItemSales.Add(novoItem);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                // Retorna a venda criada (opcional: pode retornar com os itens também)
                return Ok(novaVenda);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Erro ao cadastrar venda: {ex.Message}");
            }
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