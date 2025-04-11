using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_log_reservations")]
    public class ReportLogReservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Today;

        [Column("reservation_id")]
        public int? ReservationId { get; set; }

        [Column("action")]
        public string? Action { get; set; }

        [Column("action_date")]
        public DateTime? ActionDate { get; set; }

        [Column("performed_by")]
        public int? PerformedBy { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }

        [ForeignKey("PerformedBy")]
        public User? User { get; set; }
    }
}