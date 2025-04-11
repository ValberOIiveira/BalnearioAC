using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_products_sold")]
    public class ReportProductSold
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

        [Column("total_quantity_sold")]
        public int? TotalQuantitySold { get; set; }

        [Column("total_revenue")]
        public decimal? TotalRevenue { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}