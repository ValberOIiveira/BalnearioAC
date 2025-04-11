using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_sales_by_employee")]
    public class ReportSalesByEmployee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Column("employee_id")]
        public int? EmployeeId { get; set; }

        [Column("employee_name")]
        public string? EmployeeName { get; set; }

        [Column("total_sales")]
        public int? TotalSales { get; set; }

        [Column("total_value")]
        public decimal? TotalValue { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}