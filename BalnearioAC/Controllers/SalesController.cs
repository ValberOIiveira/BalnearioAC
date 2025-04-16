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
        // Define a rota: GET /sales/completo
        [HttpGet("completo")]
        public async Task<ActionResult<IEnumerable<object>>> GetSalesWithItems()
        {
            // Consulta a tabela de vendas (Sales) no banco
            var salesWithItems = await _context.Sales

                // Inclui os itens de cada venda (ItemSales)
                .Include(s => s.ItemSales)

                // Para cada ItemSale, inclui também os dados do produto relacionado (Product)
                .ThenInclude(i => i.Product)

                // Projeta os dados em um novo objeto anônimo (como um DTO)
                .Select(s => new
                {
                    // Pega a data da venda
                    DataVenda = s.SaleDate,

                    // Pega o valor total da venda
                    Total = s.TotalValue,

                    // Define a forma de pagamento fixa como "Pix"
                    FormaPagamento = "Pix",

                    // Monta a lista de itens dessa venda
                    Itens = s.ItemSales.Select(i => new
                    {
                        // Se o produto ainda existir, pega o nome. Se não, mostra "Produto removido"
                        Produto = i.Product != null ? i.Product.Name : "Produto removido",

                        // Quantidade vendida
                        Quantidade = i.Quantity,

                        // Preço unitário do produto (ou 0 se produto foi removido)
                        PrecoUnitario = i.Product != null ? i.Product.Price : 0,

                        // Subtotal = preço x quantidade (ou 0 se produto foi removido)
                        Subtotal = i.Product != null ? i.Product.Price * i.Quantity : 0
                    }).ToList() // Converte para lista
                })
                .ToListAsync(); // Executa a consulta de forma assíncrona

            // Retorna o resultado como JSON
            return Ok(salesWithItems);
        }

    }
}