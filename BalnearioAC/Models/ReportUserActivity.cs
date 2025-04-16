using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_user_activity")]
    public class ReportUserActivity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Today;

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("user_name")]
        public string? UserName { get; set; }

        [Column("activity_type")]
        public string? ActivityType { get; set; }

        [Column("activity_date")]
        public DateTime? ActivityDate { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}