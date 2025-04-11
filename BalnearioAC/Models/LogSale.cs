using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("log_sales")]
    public class LogSale
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("sales")]
        [Column("id_sale")]
        public int? SaleId { get; set; }

        [Column("action")]
        public string Action { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; } = DateTime.Now;
        
        [ForeignKey("users")]
        [Column("performed_by")]
        public int? PerformedBy { get; set; }

        [Column("details")]
        public string Details { get; set; }
    }
}
