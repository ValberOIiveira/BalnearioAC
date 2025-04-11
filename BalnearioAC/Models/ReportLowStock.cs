using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_low_stock")]
    public class ReportLowStock
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Today;

        [Column("product_id")]
        public int? ProductId { get; set; }

        [Column("product_name")]
        public string? ProductName { get; set; }

        [Column("qtd")]
        public int? Qtd { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}