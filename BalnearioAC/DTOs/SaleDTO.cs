using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BalnearioAC.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal? TotalValue { get; set; }
        public string? EmployeeName { get; set; } // Nome do funcionário (Employee.User.Name)
        public List<ItemSaleDTO> ItemSales { get; set; } // Lista de itens da venda
    }


    public class ItemSaleDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Nome do produto
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }



}