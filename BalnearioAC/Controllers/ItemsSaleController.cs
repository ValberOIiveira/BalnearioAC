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
    public class ItemsSaleController : ControllerBase
    {
        private readonly Conexao _context;
        public ItemsSaleController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemSale>> getItemsSale() 
        {
            return await _context.ItemSales.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ItemSale>> postItemSale([FromBody] ItemSale itemSale)
        {
            if (itemSale == null)
            {
                return BadRequest("Item nao pode ser nulo");
            }

            _context.ItemSales.Add(itemSale);
            await _context.SaveChangesAsync();
            return itemSale;
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> deleteItemSale(int id)
        {
            var itemSale = await _context.ItemSales.FindAsync(id);
            if (itemSale == null)
            {
                return NotFound("Item nao encontrado");
            }
            _context.ItemSales.Remove(itemSale);
            await _context.SaveChangesAsync();
            return NoContent();  

        }

        [HttpPut]
        public async Task<IActionResult> putItemSale(int id,[FromBody] ItemSale itemSale) 
        {
            var existente = await _context.ItemSales.FindAsync(id);
            if(existente == null) return NotFound();

            existente.SaleId = itemSale.SaleId;
            existente.ProductId = itemSale.ProductId;
            existente.Quantity = itemSale.Quantity;
            await _context.SaveChangesAsync();
            return Ok(existente);

        }
    }
}
	