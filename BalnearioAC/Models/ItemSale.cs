using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("sales")]
    public class Sale
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_employee")]
        public int? EmployeeId { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }

        [Column("total_value")]
        public decimal? TotalValue { get; set; }

        // Relacionamento com a tabela Employee (um funcionário pode fazer várias vendas)
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        // Relacionamento com a tabela ItemSales (uma venda pode ter vários itens)
        public ICollection<ItemSale> ItemSales { get; set; } = new List<ItemSale>();
    }
}
