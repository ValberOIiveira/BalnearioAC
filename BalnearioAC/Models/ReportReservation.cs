using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_reservations")]
    public class ReportReservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Column("kiosk_id")]
        public int? KioskId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [ForeignKey("KioskId")]
        public Kiosk? Kiosk { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
