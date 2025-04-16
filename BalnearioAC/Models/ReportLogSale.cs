using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_log_sales")]
    public class ReportLogSale
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Today;

        [Column("sale_id")]
        public int? SaleId { get; set; }

        [Column("action")]
        public string? Action { get; set; }

        [Column("action_date")]
        public DateTime? ActionDate { get; set; }

        [Column("performed_by")]
        public int? PerformedBy { get; set; }

        [Column("details")]
        public string? Details { get; set; }

        [ForeignKey("SaleId")]
        public Sale? Sale { get; set; }

        [ForeignKey("PerformedBy")]
        public User? User { get; set; }
    }
}